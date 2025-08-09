using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyObjectArray[] enemies;

    private bool[] spawned;

    public float waitTime;

    void Awake()
    {
        spawned = new bool[enemies.Length];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !spawned[Current.CurrentSave.loop])
        {
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies()
    {
        spawned[Current.CurrentSave.loop] = true;
        foreach (GameObject enemy in enemies[Current.CurrentSave.loop].array)
        {
            Debug.Log("Spawned enemy");
            Instantiate(enemy, chooseRandomSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(waitTime);

        }
        
    }

    Vector3 chooseRandomSpawnPoint()
    {
        Camera cam = Camera.main;

        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        float left = screenBottomLeft.x;
        float right = screenTopRight.x;
        float bottom = screenBottomLeft.y;
        float top = screenTopRight.y;

        int edge = Random.Range(0, 4); 
        float x = 0f, y = 0f;

        switch (edge)
        {
            case 0: 
                x = Random.Range(left, right);
                y = top ;
                break;
            case 1: 
                x = Random.Range(left, right);
                y = bottom ;
                break;
            case 2: 
                x = left ;
                y = Random.Range(bottom, top);
                break;
            case 3: 
                x = right ;
                y = Random.Range(bottom, top);
                break;
        }

        return new Vector3(x, y, 0f);
    }
}
