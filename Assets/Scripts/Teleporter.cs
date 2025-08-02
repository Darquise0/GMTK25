using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination;
    [SerializeField] private bool isLR, isUD;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Transform player = col.gameObject.transform;
            if (isLR)
            {
                player.position = new Vector3(destination.position.x,player.position.y,0);
            }
            else if (isUD)
            {
                player.position = new Vector3(player.position.x,destination.position.y,0);
            }
            
        }
    }
}
