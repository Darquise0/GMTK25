using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScene : MonoBehaviour
{
    public string sceneName;

    public PlayerData playerData;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Lights playerLights =collision.gameObject.GetComponent<Lights>();
            playerData.playerIntensity =  playerLights.playerLight.intensity;
            playerData.flashlightBatteryRemaining = playerLights.getFBR();

            playerData.loopCounter = Global.loopCounter;
            playerData.evidenceCount = Global.evidenceCount;

            playerData.writtenToBefore = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}
