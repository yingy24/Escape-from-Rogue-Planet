using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Public Classes
    public PlayerAttributes playerAttributes;
    public GameObject player;
    public GameObject deathScreen;
    public GameObject winScreen;

    bool dead;
    bool win;

    Animator anim;

	// Use this for initialization
	void Awake () {
        anim = player.GetComponent<Animator>();
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
        dead = false;
        win = false;
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
            return; // Make Winning UI
        }
    }

    void LateUpdate()
    {
        if (dead)
        {
            if (Input.GetButton("Cancel") | Input.GetButton("Options"))
            {
                Time.timeScale = 1f;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level_ForReal");
            }
        }
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(3);
        dead = true;
        Time.timeScale = 0f;
        deathScreen.SetActive(true);
        
    }
}
