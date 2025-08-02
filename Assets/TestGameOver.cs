using UnityEngine;

public class TestGameOver : MonoBehaviour
{
    public GameObject gameOverScreeen;
    void Start()
    {
        PlayerMovement.freeze();
        Instantiate(gameOverScreeen, this.transform);
    }
}
