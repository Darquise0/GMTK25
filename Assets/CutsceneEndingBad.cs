using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEndingBad : Cutscene
{
    public float moveSpeed;
    public Transform target;
    private Vector3 localTarget;

    public GameObject actor;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        actor = Global.playerInstance;
        spriteRenderer = actor.GetComponent<SpriteRenderer>();
        animator = actor.GetComponent<Animator>();
        localTarget = target.position;
        StartCoroutine(triggerCutscene());
    }
    public IEnumerator triggerCutscene()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueManager.target = targets[i];
            yield return StartCoroutine(dialogueManager.startDialogue(dialogues[i]));
        }
        yield return StartCoroutine(moveRight());
        Destroy(gameObject);
        SceneManager.LoadScene("End Credits");
    }

    IEnumerator moveRight()
    {
        animator.Play("Walk-RIGHT");
        //animator.gameObject.GetComponent<Collider>().enabled=false;
        Camera.main.GetComponent<CameraFollow>().setPosition(Camera.main.transform.position);
        spriteRenderer.flipX = true;
        while (Vector3.Distance(actor.transform.position, localTarget) > 0.05f)
        {
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, localTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
