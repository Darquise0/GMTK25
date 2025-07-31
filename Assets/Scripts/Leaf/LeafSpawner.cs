using UnityEngine;

namespace ClearLeaves
{
    public class LeafSpawner : MonoBehaviour
    {
        public RectTransform spawnArea;
        public GameObject leafPrefab;
        public static int leafCount = 5;

        public void doAwake() { this.Awake(); }
        void Awake()
        {
            for (int i = 0; i < leafCount; i++)
            {
                GameObject leaf = Instantiate(leafPrefab, spawnArea);
                RectTransform leafRT = leaf.GetComponent<RectTransform>();

                float x = Random.Range(0, spawnArea.rect.width);
                float y = Random.Range(0, spawnArea.rect.height);

                leafRT.anchoredPosition = new Vector2(x, y);
            }
        }
    }
}