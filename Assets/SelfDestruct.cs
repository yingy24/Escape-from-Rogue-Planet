using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float destroyIn = 1f;

	// Update is called once per frame
	void Update () {
        destroyIn -= Time.deltaTime;
        if (destroyIn <= 0) Destroy(gameObject);
    }
}
