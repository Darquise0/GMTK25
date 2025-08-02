using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataLoader : MonoBehaviour
{
    public Lights lights;

    public PlayerData playerData;
    void Awake()
    {
        if (playerData.writtenToBefore)
        {
            if (SceneManager.GetActiveScene().name == "SampleScene") { gameObject.transform.position = playerData.playerPos + new Vector3(0,-1,0); }
            lights.loadInstance(playerData);
        }

        Global.playerInstance = gameObject;
    }
}
