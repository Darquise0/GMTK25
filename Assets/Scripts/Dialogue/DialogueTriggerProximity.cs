using UnityEngine;

public class DialogueTriggerProximity : DialogueTrigger
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered!");
        dm.trigger(this.myDialogue);
    }
}
