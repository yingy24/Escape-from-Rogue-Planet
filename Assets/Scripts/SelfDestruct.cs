using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour {

    public float lifeTime = 1f;

	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
}
