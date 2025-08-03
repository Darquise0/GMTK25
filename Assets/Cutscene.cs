using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] protected DialogueData[] dialogues;
    [SerializeField] protected DialogueManager dialogueManager;

    [SerializeField] protected GameObject[] targets;

    protected IEnumerator triggerCutscene()
    {
        PlayerMovement.freeze();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueManager.target = targets[i];
            yield return StartCoroutine(dialogueManager.startDialogue(dialogues[i]));
        }
        Destroy(gameObject);
        PlayerMovement.unfreeze();
    }

    void Update()
    {
        if (this.gameObject.GetComponent<Trigger>().isTouchingPlayer())
        { 
            StartCoroutine(triggerCutscene());
        }   
    }
}
