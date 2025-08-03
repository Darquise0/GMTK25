using TMPro;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    JournalData journalData;

    public TextMeshProUGUI[] entries = new TextMeshProUGUI[3];

    [SerializeField] AudioSource pageSound;
    public void startMinigame(JournalData jd)
    {
        prepareEntries(jd);
    }

    void prepareEntries(JournalData newJournalData)
    {
        journalData = newJournalData;
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i].text = journalData.entries[i];
        }
        pageSound.Play();
    }

    public void checkContradiction(int playerChoice)
    {
        if (playerChoice == journalData.contradictionIndex)
        {
            Global.evidenceCount++;
            PlayerMovement.unfreeze();
            this.gameObject.SetActive(false);
        }
    }
}
    