using UnityEngine;

public class TeleporterLostWoods : Teleporter
{
    public CameraFollow mainCam;
    public Vector3 newCameraPos;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = destination;
            mainCam.setPosition(newCameraPos);
        }
    }
}
