using ClearLeaves;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public ScriptableObject data;

    public LeafManager leafManager;
    public GameObject manager;

    Trigger trigger;

    void Awake()
    {
        trigger = this.gameObject.GetComponent<Trigger>();
    }

    void OnEnable()
    {
        InputManager.OnInteract += SubmitPressed;
    }

    void OnDisable()
    {
        InputManager.OnInteract -= SubmitPressed;
    }

    void SubmitPressed()
    {
        if (trigger.isTouchingPlayer())
        {

            leafManager.gameObject.SetActive(true);

            leafManager.startMinigame(data, manager);

            Destroy(this.gameObject);
        }
    }
}
