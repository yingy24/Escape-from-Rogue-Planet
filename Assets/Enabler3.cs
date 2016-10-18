using UnityEngine;
using System.Collections;

public class Enabler3 : MonoBehaviour {

    public GameObject object1;
    public GameObject object2;

    // Use this for initialization
    void OnTriggerEnter()
    {
        object1.SetActive(true);
        object2.SetActive(true);
    }
}
