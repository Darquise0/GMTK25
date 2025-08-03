using UnityEngine;

public class Trigger : MonoBehaviour
{
    bool touchingPlayer;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingPlayer = false;
        }
    }

    public bool isTouchingPlayer()
    {
        return this.touchingPlayer;
    }
}
