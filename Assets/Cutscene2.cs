using System.Collections;
using UnityEngine;

public class Cutscene2 : Cutscene
{
    public float moveSpeed;
    public Transform target;
    private Vector3 localTarget;

    public GameObject actor;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = actor.GetComponent<SpriteRenderer>();
        animator = actor.GetComponent<Animator>();
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
        Destroy(actor);
        PlayerMovement.unfreeze();
    }

    IEnumerator moveRight()
    {
        animator.SetBool("isWalking", true);
        spriteRenderer.flipX = true;
        while (Vector3.Distance(actor.transform.position, localTarget) > 0.05f)
        {
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, localTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        animator.SetBool("isWalking", false);
    }
}
