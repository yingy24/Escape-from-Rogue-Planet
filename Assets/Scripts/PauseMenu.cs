using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public string mainMenu;

    public bool isPaused;

    public GameObject pauseMenuCanvas;

    // Update is called once per frame
    void Update() {

        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        } else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }

        //Pressing Menu Key
        if (Input.GetButtonDown("Menu"))
        {
            isPaused = !isPaused;
        }
    }

    //For onscreen Pause button
    public void Pause()
    {
        isPaused = !isPaused;
    }

    public void Resume()
    {
        isPaused = false;
    }

    public void quitToMainMenu(string mainMenu)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
    }
}
