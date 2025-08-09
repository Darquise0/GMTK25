using UnityEngine;

public class LoopObject : MonoBehaviour
{
    public int activeLoopNumber;
    public void Start()
    {
        if (Current.CurrentSave.loop != activeLoopNumber)
        {
            Destroy(gameObject);
        }
    }
}
