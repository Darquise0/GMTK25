using UnityEngine;

public class LoopObject : MonoBehaviour
{
    public int activeLoopNumber;
    void Awake()
    {
        if (Global.loopCounter != activeLoopNumber)
        {
            this.gameObject.SetActive(false);
        }
    }
}
