using UnityEngine;

public class DialogueTriggerInteract : DialogueTrigger
{
    bool isTouchingPlayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
        }
    }
    void Update()
    {
        if(isTouchingPlayer && InputManager.Interaction)
        {
            Debug.Log("Entered!");
            dm.trigger(this.myDialogue);
        } 
    }
}
