using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalFlee : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector3 playerPosition;

    public void SetFleeTarget(Vector3 playerPos)
    {
        playerPosition = playerPos;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 fleeDirection = (transform.position - playerPosition).normalized;
        rb.linearVelocity = fleeDirection * speed;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = fleeDirection.x < 0;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

