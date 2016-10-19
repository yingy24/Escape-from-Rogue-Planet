using UnityEngine;
using System.Collections;

public class Enabler2 : MonoBehaviour {

    public GameObject object1;

    // Use this for initialization
	void Start () {
        object1.SetActive(true);
	}
}
