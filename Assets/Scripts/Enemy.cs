using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float movementSpeed = 2f;
    Rigidbody2D rb;
    [SerializeField] public Transform target;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            // Can be used to rotate the enemy when the follow:
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
    }
}
