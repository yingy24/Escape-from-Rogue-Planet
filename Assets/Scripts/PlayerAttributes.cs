using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    //Public Member Variables
    public float health;
    public Slider playerHealth;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        playerHealth.value = health;
	}
}
