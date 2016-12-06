using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    public Button newLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
          
	}

    public void LevelLoad()
    {
        Application.LoadLevel(1);
    }
}
