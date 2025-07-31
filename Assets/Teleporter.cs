using UnityEngine;

public class Teleporter : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
        }
    }
}
