using UnityEngine;

[CreateAssetMenu(fileName = "JournalData", menuName = "David's Objects/JournalData")]
public class JournalData : ScriptableObject
{
    public string[] entries = new string[3];

    [Tooltip("Indexed 1")]
    public int contradictionIndex;
}
