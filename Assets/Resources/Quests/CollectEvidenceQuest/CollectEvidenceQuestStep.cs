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
            UpdateState();
        }

        if (evidenceCollected >= evidenceToComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = evidenceCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.evidenceCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
