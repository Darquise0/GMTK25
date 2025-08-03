using System.Collections;
using UnityEngine;

public class CutsceneInitial : Cutscene
{
    public float moveSpeed = 2f;
    public Transform target;
    private Vector3 localTarget;

    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        localTarget = target.position;
        StartCoroutine(triggerCutscene());
    }
    public IEnumerator triggerCutscene()
    {
        PlayerMovement.freeze();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueManager.target = targets[i];
            yield return StartCoroutine(dialogueManager.startDialogue(dialogues[i]));
        }
        yield return StartCoroutine(moveRight());
        Destroy(gameObject);
        PlayerMovement.unfreeze();
    }

    IEnumerator moveRight()
    {
        float elapsed = 0f;
        animator.SetBool("isWalking", true);
        spriteRenderer.flipX=true;
        while (gameObject.transform.position.x < localTarget.x)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        animator.SetBool("isWalking",false);
    }
}
