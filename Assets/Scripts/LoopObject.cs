using UnityEngine;

public class LoopObject : MonoBehaviour
{
    public int activeLoopNumber;
    public void Start()
    {
        if (Global.loopCounter != activeLoopNumber)
        {
            Destroy(gameObject);
        }
    }
}
