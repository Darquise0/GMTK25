using UnityEngine;

public class SetGlobalActive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<Global>().enabled = true;
    }

}
