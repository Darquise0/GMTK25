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
            Global.save();
            SceneManager.LoadScene(sceneName);
        }
    }
}
