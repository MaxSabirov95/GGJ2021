using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Level Scene");
        Time.timeScale = 1f;        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
