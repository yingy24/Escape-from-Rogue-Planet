using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Public Classes
    public FreeCamera cameraScript;


    //Public Member variables
    public PlayerAttributes playerAttributes;
    public GameObject player;
    public GameObject camera;
    public GameObject deathScreen;
    public GameObject winScreen;
    public GameObject pauseMenu;

    public GameObject keyboard, controller, optionsMenu;

    bool dead;
    bool win;

    Animator anim;

	// Use this for initialization
	void Awake () {
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
        dead = false;
        win = false;
        anim = player.GetComponent<Animator>();
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (playerAttributes.isDead)
        {
            anim.SetBool("Dead", true);
            StartCoroutine(WaitForAnimation());
            return;
        }
        if (playerAttributes.hasWon)
        {
            win = true;
            winScreen.SetActive(true);
            Time.timeScale = 0;
            return; // Make Winning UI
        }
        if (Input.GetButtonDown("Options"))
        {
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    void LateUpdate()
    {
        if (dead)
        {
            if (Input.GetButton("Options"))
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level_ForReal");
            }
        }

        if (win)
        {
            if (Input.GetButton("Options"))
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void Controller()
    {
        keyboard.SetActive(false);
        optionsMenu.SetActive(false);
        cameraScript.useMouse = false;
        controller.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void Keyboard()
    {
        controller.SetActive(false);
        optionsMenu.SetActive(false);
        cameraScript.useMouse = true;
        keyboard.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(3);
        dead = true;
        Time.timeScale = 0f;
        deathScreen.SetActive(true);
        
    }
}
