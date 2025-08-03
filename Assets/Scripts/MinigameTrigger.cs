using ClearLeaves;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public ScriptableObject data;

    public LeafManager leafManager;
    public GameObject manager;

    bool isCollected;
    void Update()
    {
        if (this.gameObject.GetComponent<Trigger>().isTouchingPlayer() && InputManager.Interaction)
        {
            leafManager.gameObject.SetActive(true);
            PlayerMovement.freeze();

            leafManager.GetComponent<LeafManager>().startMinigame(data, manager);

            Destroy(this.gameObject);
        }
    }

    
}
