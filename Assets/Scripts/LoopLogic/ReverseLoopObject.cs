using UnityEngine;

public class ReverseLoopObject : MonoBehaviour
{
    public int deactiveLoopNumber;
    public void Start()
    {
        if (Global.loopCounter == deactiveLoopNumber)
        {
            Destroy(gameObject);
        }
    }
}
