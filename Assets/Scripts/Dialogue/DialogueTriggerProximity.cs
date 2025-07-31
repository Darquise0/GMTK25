using UnityEngine;

public class DialogueTriggerProximity : DialogueTrigger
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered!");
            dm.trigger(this.myDialogue);
        } 
    }
}
