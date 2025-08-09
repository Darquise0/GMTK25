using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VisitJournalsQuestStep : QuestStep
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
