using System;
using UnityEngine;

public static class SaveSystem
{
    private const string SaveKey = "GameSave";

    // Turn this on to stop accidental "empty" saves from overwriting good data.
    public static bool PreventEmptyOverwrite = true;

    public static void OnSave(SaveData data, bool force = false)
    {
        if (data == null)
        {
            Debug.LogWarning("[SaveSystem] OnSave called with null data.\n" + Environment.StackTrace);
            return;
        }

        if (PreventEmptyOverwrite && !force && data.LooksUninitialized())
        {
            Debug.LogWarning("[SaveSystem] Blocked EMPTY save overwrite. Caller:\n" + Environment.StackTrace);
            return;
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + json);
    }

    public static SaveData OnLoad()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            string json = PlayerPrefs.GetString(SaveKey);
            var data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game Loaded: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("No save data found.");
            return null;
        }
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey(SaveKey);
    }
}
