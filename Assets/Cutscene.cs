using System.Collections;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] DialogueData[] dialogues;
    [SerializeField] DialogueManager dialogueManager;

    [SerializeField] GameObject[] targets;

    IEnumerator triggerCutscene()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueManager.target = targets[i];
            yield return StartCoroutine(dialogueManager.startDialogue(dialogues[i]));
        }
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(triggerCutscene());
    }
}
