using UnityEngine;

public class LayerSorting : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer treeRenderer;
    public Transform player;

    [Header("Settings")]
    public float cutoffY = -1f;
    public string layerAbovePlayer = "Tree";
    public string layerBelowPlayer = "Ground";

    void Update()
    {
        if (player == null || treeRenderer == null) return;

        if (player.position.y > transform.position.y + cutoffY)
        {
            treeRenderer.sortingLayerName = layerAbovePlayer;
        }
        else
        {
            treeRenderer.sortingLayerName = layerBelowPlayer;
        }
    }
}

