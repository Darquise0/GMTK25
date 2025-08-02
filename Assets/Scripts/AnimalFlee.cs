using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalFlee : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float idleBeforeRunTime = 2f;
    [SerializeField] private float fadeOutDuration = 1.5f;

    private Rigidbody2D rb;
    private Vector3 playerPosition;
    private Animator animator;
    public AudioSource footsteps;

    private bool isOffScreen = false;
    private bool hasFadedOut = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        float directionX = Mathf.Sign(transform.position.x - playerPosition.x);

        if (animator != null)
        {
            animator.SetBool("isRight", directionX < 0);
            animator.SetBool("isMoving", false);
        }

        StartCoroutine(FleeAfterDelay(directionX));
    }

    public void SetFleeTarget(Vector3 playerPos)
    {
        playerPosition = playerPos;
    }

    private IEnumerator FleeAfterDelay(float directionX)
    {
        yield return new WaitForSeconds(idleBeforeRunTime);

        Vector2 fleeDirection = new Vector2(directionX, 0f);
        rb.linearVelocity = fleeDirection * speed;

        if (animator != null)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isRight", directionX > 0);
        }

        if (footsteps != null)
        {
            footsteps.volume = 1f;
            footsteps.Play();
            StartCoroutine(FadeFootsteps());
        }
    }

    private IEnumerator FadeFootsteps()
    {
        float startVolume = footsteps.volume;
        float timeElapsed = 0f;

        while (timeElapsed < fadeOutDuration)
        {
            footsteps.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / fadeOutDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        footsteps.Stop();
        hasFadedOut = true;

        if (isOffScreen)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        isOffScreen = true;

        if (hasFadedOut)
        {
            Destroy(gameObject);
        }
    }
}
