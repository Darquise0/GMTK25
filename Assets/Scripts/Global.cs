using UnityEngine;
using UnityEngine.SceneManagement;

public class Global : MonoBehaviour
{
    [SerializeField]public static int loopCounter = 0;
    [SerializeField]public static int evidenceCount = 0;

    public static GameObject playerInstance;

    public static PlayerData playerData;

    public static bool visitedLW;

    void Awake()
    {
        if (playerData != null && playerData.writtenToBefore)
        {
            loopCounter = playerData.loopCounter;
            evidenceCount = playerData.evidenceCount;
            visitedLW = playerData.visitedLostWoods;
        }
    }

    public static void save()
    {
        playerData = Resources.Load<PlayerData>("PlayerData");
        Lights playerLights = playerInstance.GetComponent<Lights>();
        playerData.playerIntensity = playerLights.playerLight.intensity;
        playerData.flashlightBatteryRemaining = playerLights.getFBR();

        playerData.loopCounter = Global.loopCounter;
        playerData.evidenceCount = Global.evidenceCount;

        if (SceneManager.GetActiveScene().name == "SampleScene") { playerData.playerPos = playerInstance.transform.position; }

        playerData.writtenToBefore = true;
        playerData.visitedLostWoods = visitedLW;
    }
}
