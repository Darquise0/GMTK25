using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] protected DialogueData[] dialogues;
    [SerializeField] protected DialogueManager dialogueManager;

    [SerializeField] protected GameObject[] targets;

    public bool isInitial;

    protected IEnumerator triggerCutscene()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueManager.target = targets[i];
            yield return StartCoroutine(dialogueManager.startDialogue(dialogues[i]));
        }
        Destroy(gameObject);
    }
}
