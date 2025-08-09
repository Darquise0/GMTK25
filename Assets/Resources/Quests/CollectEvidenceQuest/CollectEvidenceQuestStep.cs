using UnityEngine;

public class CollectEvidenceQuest : QuestStep
{
    private int evidenceCollected = 0;
    private int evidenceToComplete = 2;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.onEvidenceCollected += EvidenceCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.onEvidenceCollected -= EvidenceCollected;
    }

    private void EvidenceCollected()
    {
        if (evidenceCollected < evidenceToComplete)
        {
            evidenceCollected++;
        }

        if (evidenceCollected >= evidenceToComplete)
        {
            FinishQuestStep();
        }
    }
}
