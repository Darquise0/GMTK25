using UnityEngine;
using System.Collections.Generic;

public class Combat : MonoBehaviour
{
    private Dictionary<GameObject, float> enemiesInLight = new Dictionary<GameObject, float>();
    public float destroyAfterSeconds = 10f;

    void Update()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (var kvp in new Dictionary<GameObject, float>(enemiesInLight))
        {
            if (kvp.Key == null)
            {
                toRemove.Add(kvp.Key);
                continue;
            }

            enemiesInLight[kvp.Key] += Time.deltaTime;

            if (enemiesInLight[kvp.Key] >= destroyAfterSeconds)
            {
                Destroy(kvp.Key);
                toRemove.Add(kvp.Key);
            }
        }

        foreach (GameObject enemy in toRemove)
        {
            enemiesInLight.Remove(enemy);
        }
    }

    public void OnChildTriggerEnter(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpriteRenderer sr = other.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.enabled = false;

            if (!enemiesInLight.ContainsKey(other.gameObject))
                enemiesInLight.Add(other.gameObject, 0f);
        }
    }

    public void OnChildTriggerExit(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpriteRenderer sr = other.GetComponentInChildren<SpriteRenderer>();
            if (sr != null)
                sr.enabled = true;

            enemiesInLight.Remove(other.gameObject);
        }
    }
}
