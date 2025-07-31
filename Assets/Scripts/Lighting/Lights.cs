using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System.Collections;

public class Lights : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D playerLight;
    public Light2D flashlight;
    public GameObject bar1;
    public GameObject bar2;
    public GameObject bar3;
    public GameObject bar4;
    public AudioSource flicker;
    public AudioSource click;
    public AudioSource crank;

    public float dayGlobalIntensity = 35f;
    public float nightGlobalIntensity = 0.2f;

    public float dayPlayerIntensity = 0f;
    public float nightPlayerIntensity = 2f;

    public float transitionDuration = 3f;

    private float t = 0f;
    private float globalTarget;
    private float playerTarget;

    private bool isNight = false;
    private bool flashlightIsOn = false;

    public float flashlightBatteryLife = 30f;
    private float flashlightBatteryRemaining;

    private bool isFlickering = false;
    private bool isAmbientFlickering = false;
    private bool isForcedFlickering = false;

    public bool allowAmbientFlicker = true;
    public float flickerChancePerSecond = 0.2f;
    public float flickerDuration = 0.05f;
    private bool forceAmbientFlicker = false;
    private float ambientFlickerCooldown = 0f;
    public float ambientFlickerDelay = 10f;

    public float crankInterval = 3f;
    bool isCranking = false;

    void Start()
    {
        UpdateBatteryUI();
        globalLight.intensity = dayGlobalIntensity;
        playerLight.intensity = dayPlayerIntensity;
        flashlight.intensity = 0f;

        globalTarget = dayGlobalIntensity;
        playerTarget = dayPlayerIntensity;

        flashlightBatteryRemaining = flashlightBatteryLife;
    }

    void Update()
    {
        if (ambientFlickerCooldown > 0f)
        {
            ambientFlickerCooldown -= Time.deltaTime;
        }

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            isNight = !isNight;
            globalTarget = isNight ? nightGlobalIntensity : dayGlobalIntensity;
            playerTarget = isNight ? nightPlayerIntensity : dayPlayerIntensity;
            t = 0f;
        }

        if (Keyboard.current.fKey.wasPressedThisFrame && flashlightBatteryRemaining > 0f)
        {
            click.Play();
            flashlightIsOn = !flashlightIsOn;
            if (!flashlightIsOn)
            {
                Debug.Log("Flashlight turned off. Battery remaining: " + flashlightBatteryRemaining.ToString("F1") + "s");
            }
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (!flashlightIsOn && flashlightBatteryRemaining < flashlightBatteryLife)
            {
                StartCoroutine(CrankRecharge());
            }
            else
            {
                Debug.Log("Cannot crank: flashlight is on or battery is full.");
            }
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            StartCoroutine(ForcedFlickerEffect());
            Debug.Log("Forced flicker triggered.");
        }

        if (Mathf.Abs(globalLight.intensity - globalTarget) > 0.01f)
        {
            t += Time.deltaTime / transitionDuration;
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, globalTarget, t);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, playerTarget, t);
        }

        if (flashlightIsOn && flashlightBatteryRemaining > 0f)
        {
            flashlightBatteryRemaining -= Time.deltaTime;

            if (flashlightBatteryRemaining <= 0f && !isFlickering)
            {
                flashlightBatteryRemaining = 0f;
                StartCoroutine(FlickerOffEffect());
            }
        }

        if (flashlightIsOn && isNight && !isFlickering && !isAmbientFlickering && !isForcedFlickering && ambientFlickerCooldown <= 0f)
        {
            if (forceAmbientFlicker || (allowAmbientFlicker && Random.value < flickerChancePerSecond * Time.deltaTime))
            {
                StartCoroutine(AmbientFlicker());
                forceAmbientFlicker = false;
                ambientFlickerCooldown = ambientFlickerDelay;
            }
        }

        if (!isFlickering && !isAmbientFlickering && !isForcedFlickering)
        {
            flashlight.intensity = (flashlightIsOn && isNight) ? 3f : 0f;
        }
        UpdateBatteryUI();
    }
    void UpdateBatteryUI()
    {
        float batteryPercent = flashlightBatteryRemaining / flashlightBatteryLife;
        int barCount = Mathf.CeilToInt(batteryPercent * 4);

        bar1.SetActive(barCount >= 1);
        bar2.SetActive(barCount >= 2);
        bar3.SetActive(barCount >= 3);
        bar4.SetActive(barCount >= 4);
    }

    public void RechargeToNextBar()
    {
        // Don't crank if flashlight is on or battery is already full
        if (flashlightIsOn || flashlightBatteryRemaining >= flashlightBatteryLife)
        {
            Debug.Log("Cannot crank: flashlight is on or battery is full.");
            return;
        }

        float barSize = flashlightBatteryLife * 0.25f;
        int currentBar = Mathf.FloorToInt(flashlightBatteryRemaining / barSize);

        if (currentBar < 4)
        {
            flashlightBatteryRemaining = Mathf.Min((currentBar + 1) * barSize, flashlightBatteryLife);
            Debug.Log("Cranked up to bar " + (currentBar + 1) + " → Battery: " + flashlightBatteryRemaining.ToString("F1") + "s");
            UpdateBatteryUI();
        }
    }

    private IEnumerator FlickerOffEffect()
    {
        if (flicker != null)
        {
            flicker.Play();
        }

        if (isFlickering) yield break;

        isFlickering = true;

        float[] flickerPattern = {
            0.1f, 0.08f, 0.15f, 0.05f,
            0.1f, 0.2f, 0.05f, 0.12f,
            0.1f, 0.3f, 0.05f
        };

        foreach (float delay in flickerPattern)
        {
            flashlight.intensity = flashlight.intensity > 0 ? 0f : 3f;
            yield return new WaitForSeconds(delay);
        }

        flashlight.intensity = 0f;
        flashlightIsOn = false;
        isFlickering = false;
        UpdateBatteryUI();
    }

    private IEnumerator AmbientFlicker()
    {
        if (flicker != null)
        {
            flicker.Play();
        }

        if (isAmbientFlickering || isForcedFlickering) yield break;

        isAmbientFlickering = true;
        Debug.Log("Ambient flicker triggered.");

        float[] flickerDelays = { 0.1f, 0.08f, 0.12f, 0.1f };

        foreach (float delay in flickerDelays)
        {
            flashlight.intensity = 0f;
            yield return new WaitForSeconds(delay);
            flashlight.intensity = 2.5f;
            yield return new WaitForSeconds(0.05f);
        }

        flashlight.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        flashlight.intensity = 3f;

        isAmbientFlickering = false;
    }

    private IEnumerator ForcedFlickerEffect()
    {
        if (flicker != null)
        {
            flicker.Play();
        }

        if (isForcedFlickering) yield break;

        isForcedFlickering = true;

        float[] flickerDelays = { 0.15f, 0.1f, 0.2f, 0.1f, 0.25f };

        foreach (float delay in flickerDelays)
        {
            flashlight.intensity = 0f;
            yield return new WaitForSeconds(delay);
            flashlight.intensity = 3f;
            yield return new WaitForSeconds(0.05f);
        }

        flashlight.intensity = 0f;
        yield return new WaitForSeconds(0.2f);
        flashlight.intensity = 3f;

        isForcedFlickering = false;
    }

    private IEnumerator CrankRecharge()
    {
        isCranking = true;
        float crankDuration = 3f;
        float soundLength = 0.813f;
        int plays = Mathf.FloorToInt(crankDuration / soundLength);

        for (int i = 0; i < plays; i++)
        {
            crank.Play();
            yield return new WaitForSeconds(soundLength);
        }

        float remainingTime = crankDuration - (plays * soundLength);
        if (remainingTime > 0)
        {
            yield return new WaitForSeconds(remainingTime);
        }

        RechargeToNextBar();
        Debug.Log("Crank complete. Battery now: " + flashlightBatteryRemaining.ToString("F1") + "s");

        isCranking = false;
    }

}
