using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class MainMenu : MonoBehaviour {

    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
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
