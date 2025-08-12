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
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }

    void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }

    void SubmitPressed(InputEventContext inputEventContext)
    {
        if (trigger.isTouchingPlayer())
        {

            leafManager.gameObject.SetActive(true);

            leafManager.startMinigame(data, manager);

            Destroy(this.gameObject);
        }
    }
}
