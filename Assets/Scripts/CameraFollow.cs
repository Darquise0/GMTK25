using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    public Transform target;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LostWoods")
        {
            setPosition(new Vector3(0, 0, -1));
        }
        else
        { 
            assignToPlayer();
        }
        
    }

    void assignToPlayer()
    {
        target = player;
    }
    public void setPosition(Vector3 newPosition)
    {
        GameObject temp = new GameObject();
        temp.transform.position = newPosition;
        target = temp.transform;
    }
    void Update () 
    {
        transform.position = new Vector3 (target.position.x + offset.x, target.position.y + offset.y, offset.z);
    }
}
