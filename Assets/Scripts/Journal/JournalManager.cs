using TMPro;
using UnityEngine;

public class JournalManager : MinigameManager
{
    JournalData journalData;

    public TextMeshProUGUI[] entries = new TextMeshProUGUI[3];

    new public void startMinigame(ScriptableObject jd)
    {
        JournalData temp = jd as JournalData;
        Debug.Log(temp.entries.ToString());
        prepareEntries(jd as JournalData);
    }

    void prepareEntries(JournalData newJournalData)
    {
        journalData = newJournalData;
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
