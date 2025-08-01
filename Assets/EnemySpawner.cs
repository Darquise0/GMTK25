using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyObjectArray[] enemies;

    public Transform[] spawnPoints;

    private bool[] spawned;

    public float waitTime;

    void Awake()
    {
        spawned = new bool[enemies.Length];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !spawned[Global.loopCounter])
        {
            StartCoroutine(spawnEnemies());
        }
    }

    IEnumerator spawnEnemies()
    {
        spawned[Global.loopCounter] = true;
        foreach (GameObject enemy in enemies[Global.loopCounter].array)
        {
            Debug.Log("Spawned enemy");
            Instantiate(enemy, chooseRandomSpawnPoint(), Quaternion.identity);
            yield return new WaitForSeconds(waitTime);

        }
        
    }

    Vector3 chooseRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}
