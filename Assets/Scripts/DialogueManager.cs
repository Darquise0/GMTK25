using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private string[] lines;
    public float textSpeed;

    public Transform playerPos;
    public GameObject dialogueBubblePrefab;

    public Dialogue tempDialogueData;

    public Canvas canvas;

    public void Start()
    {
        StartCoroutine (startDialogue(tempDialogueData));
    }

    IEnumerator startDialogue(Dialogue dialogueData)
    {
        GameObject dialogueBubble = Instantiate(dialogueBubblePrefab, playerPos.position + new Vector3(2, 4, 0), Quaternion.identity);
        dialogueBubble.transform.parent = canvas.transform;
        TextMeshProUGUI tmp = dialogueBubble.GetComponentInChildren<TextMeshProUGUI>();


        lines = dialogueData.lines;
        foreach (string line in lines)
        {
            yield return StartCoroutine(typeLine(line, tmp));
        }
        Destroy(dialogueBubble);
    }
    
    IEnumerator typeLine(string line, TextMeshProUGUI tmp)
    {
        foreach (char ch in line.ToCharArray())
        {
            tmp.text += ch;
            yield return new WaitForSeconds(textSpeed);
        }
        tmp.text = string.Empty;
    }
}
