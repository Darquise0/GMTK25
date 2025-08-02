using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System.Collections;

public class Lights : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D playerLight;
    public Light2D upFlashlight;
    public Light2D regularFlashlight;
    public PolygonCollider2D flashlightTrigger;
    public Animator animator;
    public GameObject bar1, bar2, bar3, bar4;
    public AudioSource flicker, click, crank;

    public float dayGlobalIntensity = 20f;
    public float nightGlobalIntensity = 0.1f;
    public float dayPlayerIntensity = 0f;
    public float nightPlayerIntensity = 2f;
    public float transitionDuration = 3f;

    private float t = 0f;
    private float globalTarget;
    private float playerTarget;
    private bool isNight = false;
    private bool flashlightIsOn = false;
    public float flashlightBatteryLife = 30f;
    [SerializeField] private float flashlightBatteryRemaining;

    private bool isFlickering = false;
    private bool isAmbientFlickering = false;
    private bool isForcedFlickering = false;
    public bool allowAmbientFlicker = true;
    public float flickerChancePerSecond = 0.2f;
    public float flickerDuration = 0.05f;
    private float ambientFlickerCooldown = 0f;
    public float ambientFlickerDelay = 10f;
    public float crankInterval = 3f;

    public float dimRate = 0.1f;
    public float minPlayerLightIntensity = 0f;
    [HideInInspector] public bool isInSafeZone = false;
    private float restoreTimer = 0f;
    public float restoreDelay = 1f;
    public float restoreAmount = 0.2f;
    public float maxIntensity = 2f;

    private Light2D activeFlashlight;

    void Start()
    {
        isNight = true;
        playerTarget = nightPlayerIntensity;
        playerLight.intensity = nightPlayerIntensity;

        UpdateBatteryUI();
        globalLight.intensity = nightGlobalIntensity;
        flashlightBatteryRemaining = flashlightBatteryLife;

        globalTarget = nightGlobalIntensity;

        flashlightIsOn = false;
        if (animator != null)
        {
            animator.SetBool("FlashlightOn", false);
        }
        if (flashlightTrigger != null)
        {
            flashlightTrigger.enabled = false;
        }
        SetDirectionDown(false);
    }

    void Update()
    {
        if (ambientFlickerCooldown > 0f)
            ambientFlickerCooldown -= Time.deltaTime;

        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            isNight = !isNight;
            globalTarget = isNight ? nightGlobalIntensity : dayGlobalIntensity;
            // playerTarget = isNight ? nightPlayerIntensity : dayPlayerIntensity;
            t = 0f;
        }

        if (Keyboard.current.fKey.wasPressedThisFrame && flashlightBatteryRemaining > 0f)
        {
            click.Play();
            flashlightIsOn = !flashlightIsOn;
            flashlightTrigger.enabled = flashlightIsOn;
            animator.SetBool("FlashlightOn", flashlightIsOn);
            UpdateFlashlightState();
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (!flashlightIsOn && flashlightBatteryRemaining < flashlightBatteryLife)
                StartCoroutine(CrankRecharge());
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            playerLight.intensity = nightPlayerIntensity;
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
            if (allowAmbientFlicker && Random.value < flickerChancePerSecond * Time.deltaTime)
            {
                StartCoroutine(AmbientFlicker());
                ambientFlickerCooldown = ambientFlickerDelay;
            }
        }

        if (!isFlickering && !isAmbientFlickering && !isForcedFlickering && activeFlashlight != null)
            activeFlashlight.intensity = (flashlightIsOn && isNight) ? 3f : 0f;

        if (isInSafeZone && playerLight.intensity < maxIntensity)
        {
            restoreTimer += Time.deltaTime;
            if (restoreTimer >= restoreDelay)
            {
                playerLight.intensity = Mathf.Min(playerLight.intensity + restoreAmount, maxIntensity);
                restoreTimer = 0f;
            }
        }
        else restoreTimer = 0f;

        UpdateBatteryUI();
    }

    void UpdateFlashlightState()
    {
        if (upFlashlight) upFlashlight.enabled = flashlightIsOn && upFlashlight.gameObject.activeSelf;
        if (regularFlashlight) regularFlashlight.enabled = flashlightIsOn && regularFlashlight.gameObject.activeSelf;
    }

    public void SetDirectionDown(bool isDown)
    {
        if (isDown)
        {
            regularFlashlight.gameObject.SetActive(true);
            upFlashlight.gameObject.SetActive(false);
            activeFlashlight = regularFlashlight;
        }
        else
        {
            upFlashlight.gameObject.SetActive(true);
            regularFlashlight.gameObject.SetActive(false);
            activeFlashlight = upFlashlight;
        }
        UpdateFlashlightState();
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
        if (flashlightIsOn || flashlightBatteryRemaining >= flashlightBatteryLife) return;

        float barSize = flashlightBatteryLife * 0.25f;
        int currentBar = Mathf.FloorToInt(flashlightBatteryRemaining / barSize);

        if (currentBar < 4)
        {
            flashlightBatteryRemaining = Mathf.Min((currentBar + 1) * barSize, flashlightBatteryLife);
            UpdateBatteryUI();
        }
    }

    public void RestoreLight(float amount)
    {
        playerLight.intensity = Mathf.Clamp(playerLight.intensity + amount, 0f, nightPlayerIntensity);
    }

    public void DimPlayerLight()
    {
        playerLight.intensity = Mathf.Max(playerLight.intensity - dimRate, minPlayerLightIntensity);
    }

    public void ResetPlayerLight()
    {
        playerLight.intensity = nightPlayerIntensity;
    }

    public void loadInstance(PlayerData playerData)
    {
        playerLight.intensity = playerData.playerIntensity;
        flashlightBatteryRemaining = playerData.flashlightBatteryRemaining;
    }

    public float getFBR()
    {
        return flashlightBatteryRemaining;
    }

    private IEnumerator FlickerOffEffect()
    {
        if (isFlickering) yield break;
        isFlickering = true;
        if (flicker) flicker.Play();

        float[] pattern = { 0.1f, 0.08f, 0.15f, 0.05f, 0.1f, 0.2f, 0.05f };
        foreach (float delay in pattern)
        {
            activeFlashlight.intensity = activeFlashlight.intensity > 0 ? 0f : 3f;
            yield return new WaitForSeconds(delay);
        }

        animator.SetBool("FlashlightOn", false);
        activeFlashlight.intensity = 0f;
        flashlightIsOn = false;
        flashlightTrigger.enabled = false;
        isFlickering = false;
        UpdateBatteryUI();
    }

    private IEnumerator AmbientFlicker()
    {
        if (isAmbientFlickering || isForcedFlickering) yield break;
        isAmbientFlickering = true;
        if (flicker) flicker.Play();

        float[] delays = { 0.1f, 0.08f, 0.12f, 0.1f };
        foreach (float d in delays)
        {
            activeFlashlight.intensity = 0f;
            yield return new WaitForSeconds(d);
            activeFlashlight.intensity = 2.5f;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.1f);
        activeFlashlight.intensity = 3f;
        isAmbientFlickering = false;
    }

    private IEnumerator CrankRecharge()
    {
        float crankDuration = 3f;
        float soundLength = 0.813f;
        int plays = Mathf.FloorToInt(crankDuration / soundLength);

        for (int i = 0; i < plays; i++)
        {
            crank.Play();
            yield return new WaitForSeconds(soundLength);
        }

        float remaining = crankDuration - (plays * soundLength);
        if (remaining > 0)
            yield return new WaitForSeconds(remaining);

        RechargeToNextBar();
    }
}
