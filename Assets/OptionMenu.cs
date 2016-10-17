using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour {

    public GameObject pauseMenu;

    GameObject player;
    bool paused;

    // Use this for initialization
	void Awake () {
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!paused)
            return;

        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }

        //Pressing Esc or Start
        if (Input.GetButton("Cancel") | Input.GetButton("Options"))
        {
            paused = true;
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Quit()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
