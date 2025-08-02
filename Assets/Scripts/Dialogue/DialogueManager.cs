using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private string[] lines;
    public float textSpeed;

    public GameObject target;
    public GameObject dialogueBubblePrefab;
    public Canvas canvas;
    public Vector3 dbOffset;

    public void trigger(DialogueData dialogue)
    {
        StartCoroutine(startDialogue(dialogue));
    }
    public IEnumerator startDialogue(DialogueData dialogueData)
    {
        PlayerMovement.freeze();

        GameObject dialogueBubble = Instantiate(dialogueBubblePrefab, target.transform.position + dbOffset, Quaternion.identity);
        dialogueBubble.transform.parent = canvas.transform;
        TextMeshProUGUI tmp = dialogueBubble.GetComponentInChildren<TextMeshProUGUI>();

        lines = dialogueData.lines;
        foreach (string line in lines)
        {
            yield return StartCoroutine(typeLine(line, tmp));
            yield return new WaitUntil(() => !InputManager.Interaction);
        }
        Destroy(dialogueBubble);
        PlayerMovement.unfreeze();
    }

    IEnumerator typeLine(string line, TextMeshProUGUI tmp)
    {
        tmp.text = string.Empty;
        int index = 0;
        char[] lineArray = line.ToCharArray();

        while (index < lineArray.Length && !InputManager.Interaction)
        {
            tmp.text += lineArray[index];
            index++;

            float timer = 0f;
            while (timer < textSpeed && !InputManager.Interaction)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        yield return new WaitUntil(() => !InputManager.Interaction);
        tmp.text = line;
        yield return new WaitUntil(() => InputManager.Interaction);
    }
}
