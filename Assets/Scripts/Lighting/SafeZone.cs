using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public CircleCollider2D trigger;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Lights lights = other.GetComponent<Lights>();
            if (lights != null)
            {
                lights.isInSafeZone = true;
            }
        }

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null) enemy.ReturnToStart();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("player out");
            Lights lights = other.GetComponent<Lights>();
            if (lights != null) lights.isInSafeZone = false;
        }
    }
}
