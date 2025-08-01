using UnityEngine;

public class PlayerDataLoader : MonoBehaviour
{
    public Lights lights;

    public PlayerData playerData;
    void Awake()
    {
        if (playerData.writtenToBefore)
        {
            lights.loadInstance(playerData);
        }

        Global.playerInstance = gameObject;
    }
}
