using System.Collections;
using UnityEngine;

public class LostWoodsNoise : MonoBehaviour
{
    public AudioSource source;
    private Coroutine fadeInCo;

    [SerializeField] private float maxVolume;
    void Start()
    {
        source.Play();
        source.Pause();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(fadeInCo!=null){ StopCoroutine(fadeInCo); fadeInCo = null; }
            source.UnPause();
            fadeInCo=StartCoroutine(FadeIn());
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(fadeInCo!=null){ StopCoroutine(fadeInCo); fadeInCo = null; }
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float timeElapsed = 0f;

        float duration = 5f;
        duration = 5f - duration * source.volume;

        float startVolume = source.volume;
        while (timeElapsed < duration)
        {
            source.volume = Mathf.Lerp(startVolume, maxVolume, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float timeElapsed = 0f;
        float duration = 5f;
        duration = 5f - duration * (1-source.volume);

        float startVolume = source.volume;
        while (timeElapsed < duration)
        {
            source.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        source.Pause();
    }
}
