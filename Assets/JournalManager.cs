using TMPro;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    public JournalData journalData;

    public TextMeshProUGUI[] entries = new TextMeshProUGUI[3];
    void Start()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].text = journalData.entries[i];
        }
    }

    public void checkContradiction(int playerChoice)
    {
        if (playerChoice == journalData.contradictionIndex)
        {
            this.gameObject.SetActive(false);
        }
    }
}
