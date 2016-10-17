using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_ForReal");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
