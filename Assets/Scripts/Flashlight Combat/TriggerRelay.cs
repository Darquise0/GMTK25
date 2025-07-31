using UnityEngine;

public class TriggerRelay : MonoBehaviour
{
    public Combat parentScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        parentScript.OnChildTriggerEnter(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        parentScript.OnChildTriggerExit(other);
    }
}
