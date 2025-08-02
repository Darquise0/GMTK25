using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void quit()
    {
        Application.Quit();
    }

    public void retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
