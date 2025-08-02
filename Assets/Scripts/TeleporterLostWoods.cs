using UnityEngine;

public class TeleporterLostWoods : Teleporter
{
    public CameraFollow mainCam;
    [SerializeField]private float offset = 28f;

    public int roomNum, targetRoomNum;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Transform player = col.gameObject.transform;
            if (isLR)
            {
                player.position = new Vector3(destination.position.x, player.position.y, 0);
            }
            else if (isUD)
            {
                player.position = new Vector3(player.position.x + offset*(targetRoomNum-roomNum), destination.position.y, 0);
            }
            
            mainCam.setPosition(new Vector3((targetRoomNum-1)*offset, 0, -1));
        }
    }
}
