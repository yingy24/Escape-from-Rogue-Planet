using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject invert;
    public Button invertX, invertY ;
    public UpdatedCamera uCamera;

    GameObject player;
    bool paused;

    // Use this for initialization
	void Awake () {
        paused = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {

    }

    public void Resume()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        invert.SetActive(false);
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

    public void Invert()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        invert.SetActive(true);
    }

    //Inverted Options
    public void InvertX()
    {
        if (invertX.image.color == Color.red)
            invertX.image.color = Color.white;
        else
            invertX.image.color = Color.red;

    }

    public void InvertY()
    {
        if (invertY.image.color == Color.red)
            invertY.image.color = Color.white;
        else
            invertY.image.color = Color.red;

    }
}
