using UnityEngine;
using UnityEngine.UI;

namespace ClearLeaves
{
    public class LeafManager : MonoBehaviour
    {
        private int totalLeaves;
        public LeafSpawner leafSpawner;

        public bool isRunning;

        private GameObject nextManager;
        private ScriptableObject data;

        public Sprite radioSprite, journalSprite;

        public void startMinigame(ScriptableObject data, GameObject nextManager)
        {
            if (!isRunning)
            {
                Image imagee = gameObject.transform.GetChild(0).GetComponent<Image>();
                if (data.GetType().Name == "JournalData")
                {
                    imagee.sprite = journalSprite;
                }
                else
                {
                    imagee.sprite = radioSprite;
                }
                isRunning = true;
                totalLeaves = LeafSpawner.leafCount;
                leafSpawner.doAwake();
                this.data = data;
                this.nextManager = nextManager;
            }
        }

        public void LeafCleared()
        {
            totalLeaves--;

            if (totalLeaves <= 0)
            {
                isRunning = false;

                nextManager.SetActive(true);
                if (data.GetType().Name == "JournalData")
                {
                    nextManager.GetComponent<JournalManager>().startMinigame(data as JournalData);
                }
                else
                {
                    nextManager.GetComponent<WaveManager>().startMinigame(data as WaveData);
                }

                this.gameObject.SetActive(false); 
            }
        }
    }
}