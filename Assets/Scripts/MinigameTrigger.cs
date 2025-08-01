using ClearLeaves;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public ScriptableObject data;

    public GameObject manager;

    bool isTouchingPlayer;

    bool isCollected;
    void Update()
    {
        if (isTouchingPlayer && InputManager.Interaction)
        {
            manager.gameObject.SetActive(true);
            PlayerMovement.freeze();
            if (data == null)
            {
                manager.GetComponent<LeafManager>().startMinigame();
            }
            else if (data.GetType().Name == "JournalData")
            {
                manager.GetComponent<JournalManager>().startMinigame(data as JournalData);
            }
            else
            {
                manager.GetComponent<WaveManager>().startMinigame(data as WaveData);
            }
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
        }
    }
}
