using UnityEngine;
using System.Collections;

public class InstaDead : MonoBehaviour {

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerAttributes>().health -= 5000;
        }
    }
}
