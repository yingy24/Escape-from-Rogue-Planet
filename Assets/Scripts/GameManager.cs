using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Public Classes
    public PlayerAttributes playerAttributes;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (playerAttributes.isDead)
        {
            Time.timeScale = 0;
            return; //Make Dead UI
        }
        if (playerAttributes.hasWon)
        {
            return; // Make Winning UI
        }


    }
}
