using ClearLeaves;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public ScriptableObject data;

    public LeafManager leafManager;
    public GameObject manager;

    bool isTouchingPlayer;

    bool isCollected;
    void Update()
    {
        if (isTouchingPlayer && InputManager.Interaction)
        {
            leafManager.gameObject.SetActive(true);
            PlayerMovement.freeze();

            leafManager.GetComponent<LeafManager>().startMinigame(data, manager);

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
