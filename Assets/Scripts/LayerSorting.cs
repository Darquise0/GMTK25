using UnityEngine;

public class LayerSorting : MonoBehaviour
{
    public SpriteRenderer treeRenderer;
    public string playerTag = "Player";

    public float cutoffY = -1f;
    public string layerAbovePlayer = "Tree";
    public string layerBelowPlayer = "Ground";

    private Transform playerInRange = null;

    void Update()
    {
        if (playerInRange == null || treeRenderer == null) return;

        if (playerInRange.position.y > transform.position.y + cutoffY)
        {
            treeRenderer.sortingLayerName = layerAbovePlayer;
        }
        else
        {
            treeRenderer.sortingLayerName = layerBelowPlayer;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && other.transform == playerInRange)
        {
            playerInRange = null;
        }
    }
}
