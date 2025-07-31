using UnityEngine;

public class DialogueTriggerProximity : DialogueTrigger
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Entered!");
        dm.trigger(this.myDialogue);
    }
}
