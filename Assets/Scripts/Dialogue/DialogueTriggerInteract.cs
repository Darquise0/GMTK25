using UnityEngine;

public class DialogueTriggerInteract : DialogueTrigger
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && InputManager.Interaction)
        {
            Debug.Log("Entered!");
            dm.trigger(this.myDialogue);
        } 
    }
}
