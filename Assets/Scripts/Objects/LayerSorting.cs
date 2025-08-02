using UnityEngine;

public class LayerSorting : MonoBehaviour
{
    public SpriteRenderer treeRenderer;
    public string playerTag = "Player";

    public float cutoffY = -1f;
    public string layerAbovePlayer = "Trees";
    public string layerBelowPlayer = "Ground";

    public bool isOOB = false;

    private Transform player;

    void Start()
    {
        if (isOOB)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (treeRenderer == null) return;
        if (isOOB)
        {
            if (player == null) return;

            if (player.position.y > transform.position.y + cutoffY)
                treeRenderer.sortingLayerName = layerAbovePlayer;
            else
                treeRenderer.sortingLayerName = layerBelowPlayer;
        }
        else if (player != null)
        {
            if (player.position.y > transform.position.y + cutoffY)
                treeRenderer.sortingLayerName = layerAbovePlayer;
            else
                treeRenderer.sortingLayerName = layerBelowPlayer;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOOB && other.CompareTag(playerTag))
        {
            player = other.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!isOOB && other.CompareTag(playerTag) && other.transform == player)
        {
            player = null;
        }
    }
}
