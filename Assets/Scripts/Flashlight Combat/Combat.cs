using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Combat : MonoBehaviour
{
    private Dictionary<GameObject, float> enemiesInLight = new Dictionary<GameObject, float>();
    public float destroyAfterSeconds = 10f;
    public Lights playerLights;
    public Image healthBarFillImage;
    public AudioSource rustle;


    void Update()
    {
        if (playerLights != null && healthBarFillImage != null)
        {
            float fillAmount = 1f - Mathf.InverseLerp(0f, playerLights.nightPlayerIntensity, playerLights.playerLight.intensity);
            healthBarFillImage.fillAmount = fillAmount;
        }
        
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
                rustle.Play();
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
            Enemy enemyScript = other.GetComponent<Enemy>();
            Lights lights = GetComponent<Lights>();
            if (enemyScript != null && lights != null)
            {
                if (!enemyScript.hasMadeDecision)
                {
                    enemyScript.TryConvertToAnimal();
                }

                SpriteRenderer sr = other.GetComponentInChildren<SpriteRenderer>();
                if (sr != null)
                {
                    sr.enabled = false;
                    lights.inLight = true;
                }
            }

            if (!enemiesInLight.ContainsKey(other.gameObject))
            {
                enemiesInLight.Add(other.gameObject, 0f);
            }
        }
    }

    public void OnChildTriggerExit(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Lights lights = GetComponent<Lights>();
            SpriteRenderer sr = other.GetComponentInChildren<SpriteRenderer>();
            if (sr != null && lights != null)
            {
                sr.enabled = true;
                lights.inLight = false;
            }

            enemiesInLight.Remove(other.gameObject);
        }
    }
}
