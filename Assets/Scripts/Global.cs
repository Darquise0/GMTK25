using UnityEngine;

public class Global : MonoBehaviour
{
    public static int loopCounter = 1;
    public static int evidenceCount = 0;

    public static GameObject playerInstance;

    public PlayerData playerData;

    void Awake()
    {
        if (playerData.writtenToBefore)
        {
            loopCounter = playerData.loopCounter;
            evidenceCount = playerData.evidenceCount;
        }   
    }
}
