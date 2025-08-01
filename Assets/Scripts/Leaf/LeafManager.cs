using UnityEngine;
using ClearLeaves;

namespace ClearLeaves
{
    public class LeafManager : MonoBehaviour
    {
        private int totalLeaves;
        public LeafSpawner leafSpawner;

        public bool isRunning;

        public void startMinigame()
        {
            if (!isRunning)
            {
                isRunning = true;
                totalLeaves = LeafSpawner.leafCount;
                leafSpawner.doAwake();
            }
        }

        public void LeafCleared()
        {
            totalLeaves--;

            if (totalLeaves <= 0)
            {
                isRunning = false;
                PlayerMovement.unfreeze();
                MinigameTrigger.evidenceCount++;
                this.gameObject.SetActive(false);
            }
        }
    }
}