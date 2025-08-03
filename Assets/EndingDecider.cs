using UnityEngine;

public class EndingDecider : MonoBehaviour
{

    public GameObject goodEnding, badEnding;
    public void decide()
    {
        if (Global.evidenceCount >= 8) { Instantiate(goodEnding, this.transform.position, Quaternion.identity); }
        else { Instantiate(badEnding, this.transform.position, Quaternion.identity);}
    }
}
