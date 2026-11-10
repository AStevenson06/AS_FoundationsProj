using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("01_MainSceneFrogHome");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}