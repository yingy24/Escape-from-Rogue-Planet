using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject invert;
    public Slider invertedSlider;
    public FreeCamera fCamera;

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
    public void InvertYes()
    {
        if (invertedSlider.value == 1)
        {
            fCamera.isInvert = true;
        }
        else
            fCamera.isInvert = false;
    }
}
