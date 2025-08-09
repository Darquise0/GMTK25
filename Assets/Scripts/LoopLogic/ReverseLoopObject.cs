using UnityEngine;

public class ReverseLoopObject : MonoBehaviour
{
    public int deactiveLoopNumber;
    public void Start()
    {
        if (Current.CurrentSave.loop == deactiveLoopNumber)
        {
            Destroy(gameObject);
        }
    }
}
