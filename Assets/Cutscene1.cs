using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene1 : Cutscene
{
    public float moveSpeed;
    public Transform target;
    private Vector3 localTarget;

    public GameObject actor;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public PlayerData playerData;

    private bool played;

    void Awake()
    {
        spriteRenderer = actor.GetComponent<SpriteRenderer>();
        animator = actor.GetComponent<Animator>();
        localTarget = target.position;
        animator.Play("Monster2_Idle");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && !played)
        { 
            played = true;
            StartCoroutine(triggerCutscene());
        }
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
        yield return StartCoroutine(moveUp());
        
        
        
    }

    IEnumerator moveUp()
    {
        animator.Play("Monster2_WalkLeft");
        spriteRenderer.flipX = true;
        while (Vector3.Distance(actor.transform.position, localTarget) > 0.05f)
        {
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, localTarget, moveSpeed * Time.deltaTime);
            yield return null;
        }

        
        Global.loopCounter++;
        Global.save();
        PlayerMovement.unfreeze();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }
}
