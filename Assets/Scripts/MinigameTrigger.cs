using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public ScriptableObject data;

    public MinigameManager mm;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && InputManager.Interaction)
        {
            if (data == null) { mm.startMinigame(); }
            else { mm.startMinigame(data); }

            mm.gameObject.SetActive(true);
        }   
    }
}
