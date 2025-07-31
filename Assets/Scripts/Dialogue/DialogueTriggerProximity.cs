using UnityEngine;

public class DialogueTriggerProximity : DialogueTrigger
{
    void OnCollisionEnter(Collision collision)
    {
        dm.trigger(this.myDialogue);
    }
}
