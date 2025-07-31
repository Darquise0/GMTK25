using UnityEngine;

public class DialogueTriggerInteract : DialogueTrigger
{
    void OnCollisionStay(Collision collision)
    {
        if (InputManager.Interaction)
        {
            dm.trigger(this.myDialogue);
        }
    }
}
