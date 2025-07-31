using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class DayNightCycle : MonoBehaviour
{
    public Light2D globalLight;     // Drag your Global Light 2D here
    public Light2D playerLight;     // Drag your Player's Spot Light 2D here

    public float dayGlobalIntensity = 35f;
    public float nightGlobalIntensity = 0.2f;

    public float dayPlayerIntensity = 0f;
    public float nightPlayerIntensity = 2f;

    public float transitionDuration = 3f;

    private float t = 0f;
    private bool isNight = false;
    private float globalTarget;
    private float playerTarget;

    void Start()
    {
        globalLight.intensity = dayGlobalIntensity;
        playerLight.intensity = dayPlayerIntensity;
    }

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            isNight = !isNight;

            globalTarget = isNight ? nightGlobalIntensity : dayGlobalIntensity;
            playerTarget = isNight ? nightPlayerIntensity : dayPlayerIntensity;
            t = 0f;
        }

        if (Mathf.Abs(globalLight.intensity - globalTarget) > 0.01f)
        {
            t += Time.deltaTime / transitionDuration;

            globalLight.intensity = Mathf.Lerp(globalLight.intensity, globalTarget, t);
            playerLight.intensity = Mathf.Lerp(playerLight.intensity, playerTarget, t);
        }
    }
}
