using UnityEngine;

public class Footprints : MonoBehaviour
{
    public Collider2D flashlightCollider;
    private SpriteRenderer spriteRenderer;
    private Collider2D ownCollider;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ownCollider = GetComponent<Collider2D>();

        if (spriteRenderer == null || ownCollider == null)
        {
            Debug.LogError("Missing SpriteRenderer or Collider2D on " + gameObject.name);
        }
    }

    void Update()
    {
        if (flashlightCollider != null && ownCollider != null)
        {
            bool isLit = flashlightCollider.IsTouching(ownCollider);
            spriteRenderer.enabled = isLit;
        }
    }
}
