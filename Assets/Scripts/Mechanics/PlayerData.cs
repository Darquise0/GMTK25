using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "David's Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int evidenceCount;
    public int loopCounter;

    public float playerIntensity;

    public float flashlightBatteryRemaining;

    public Vector3 playerPos;

    public bool writtenToBefore;

    public bool visitedLostWoods;
}
