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

        float directionX = Mathf.Sign(transform.position.x - playerPosition.x);
        Vector2 fleeDirection = new Vector2(directionX, 0f);

        rb.linearVelocity = fleeDirection * speed;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = directionX < 0;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

