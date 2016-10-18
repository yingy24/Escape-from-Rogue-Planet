using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    GameObject player;
    bool paused;

    // Use this for initialization
	void Awake () {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void Options()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
