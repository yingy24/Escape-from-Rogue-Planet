using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_ForReal");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
