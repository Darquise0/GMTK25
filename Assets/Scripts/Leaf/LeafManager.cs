using UnityEngine;
using ClearLeaves;

namespace ClearLeaves
{
    public class LeafManager : MonoBehaviour
    {
        private int totalLeaves;

        void Awake()
        {
            totalLeaves = LeafSpawner.leafCount;
        }

        public void LeafCleared()
        {
            totalLeaves--;

            if (totalLeaves <= 0)
            {
                Debug.Log("You cleared all the leaves");
            }
        }
    }
}