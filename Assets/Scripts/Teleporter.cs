using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2 destination;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = destination;
        }
    }
}
