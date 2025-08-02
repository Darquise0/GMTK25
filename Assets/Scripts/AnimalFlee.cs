using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalFlee : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float idleBeforeRunTime = 2f;

    private Rigidbody2D rb;
    private Vector3 playerPosition;
    private Animator animator;

    public void SetFleeTarget(Vector3 playerPos)
    {
        playerPosition = playerPos;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        float directionX = Mathf.Sign(transform.position.x - playerPosition.x);

        // Set idle direction
        if (animator != null)
        {
            animator.SetBool("isRight", directionX < 0);
            animator.SetBool("isMoving", false);
        }

        // Start flee after idle animation
        StartCoroutine(FleeAfterDelay(directionX));
    }

    private System.Collections.IEnumerator FleeAfterDelay(float directionX)
    {
        yield return new WaitForSeconds(idleBeforeRunTime);

        // Set movement direction and start running
        Vector2 fleeDirection = new Vector2(directionX, 0f);
        rb.linearVelocity = fleeDirection * speed;

        if (animator != null)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isRight", directionX > 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
