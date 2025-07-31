using TMPro;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    JournalData journalData;

    public TextMeshProUGUI[] entries = new TextMeshProUGUI[3];

    public void startMinigame(JournalData jd)
    {
        Debug.Log(jd.entries.ToString());
        prepareEntries(jd);
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
            PlayerMovement.unfreeze();
            this.gameObject.SetActive(false);
        }
    }
}
