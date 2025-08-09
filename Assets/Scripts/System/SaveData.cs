using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string currentScene;
    public float playerX, playerY;
    public float playerHealth;
    public float Battery;
    public int loop;
    public bool visitLostWoods;

    public bool LooksUninitialized()
    {
        return string.IsNullOrEmpty(currentScene)
            && Mathf.Approximately(playerX, 0f)
            && Mathf.Approximately(playerY, 0f)
            && Mathf.Approximately(playerHealth, 0f)
            && Mathf.Approximately(Battery, 0f)
            && loop == 0
            && visitLostWoods == false;
    }
}
