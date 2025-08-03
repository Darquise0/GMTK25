using UnityEngine;

public class AudioFalloff : MonoBehaviour
{
    public Transform player;
    public float maxDistance = 20f;
    public float minDistance = 1f;
    public float maxVolume = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= minDistance)
        {
            audioSource.volume = maxVolume;
        }
        else if (distance >= maxDistance)
        {
            audioSource.volume = 0f;
        }
        else
        {
            float t = Mathf.InverseLerp(maxDistance, minDistance, distance);
            audioSource.volume = t * maxVolume;
        }
    }
}
