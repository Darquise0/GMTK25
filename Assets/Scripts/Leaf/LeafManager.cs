using UnityEngine;
using ClearLeaves;

namespace ClearLeaves
{
    public class LeafManager : MinigameManager
    {
        private int totalLeaves;
        public LeafSpawner leafSpawner;

        void Awake()
        {
            totalLeaves = LeafSpawner.leafCount;
        }

        new public void startMinigame() { this.Awake();  leafSpawner.doAwake(); }

        public void LeafCleared()
        {
            totalLeaves--;

            if (totalLeaves <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}