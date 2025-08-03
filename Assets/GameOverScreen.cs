using System.Threading;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("CoolScene");
    }
}
