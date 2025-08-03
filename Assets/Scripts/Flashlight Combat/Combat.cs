using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Combat : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 2.5f;
    private Dictionary<GameObject, float> enemiesInLight = new Dictionary<GameObject, float>();
    public float destroyAfterSeconds = 10f;
    public Lights playerLights;
    public Image healthBarFillImage;
    public AudioSource rustle;
    private float targetFill;


    void Update()
    {
        if (playerLights != null && healthBarFillImage != null)
        {
            targetFill = playerLights.playerLight.intensity;
            healthBarFillImage.fillAmount = Mathf.Lerp(healthBarFillImage.fillAmount, targetFill, lerpSpeed * Time.deltaTime);
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
