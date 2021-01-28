using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Level Scene");
    }
}
