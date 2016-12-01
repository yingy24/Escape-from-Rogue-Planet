﻿using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    public float speed, damage, timeAlive, timeToDie;

    // Use this for initialization
    void Start () {

        timeAlive = Time.time;
        timeToDie = Time.time + 2;

    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.left * Time.deltaTime * speed);

        timeAlive = Time.time;

        if (timeAlive > timeToDie)
        {
            Destroy(this.gameObject);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().health -= damage;
            Destroy(this.gameObject);
           // GamePad.SetVibration(PlayerIndex.One, 0, 1);
          //  StartCoroutine(ZeroVibrateAndSelfDestruct());
        }
        else if(other.tag == "Player")
        {
            print(other.name);
            other.GetComponent<PlayerAttributes>().health -= damage;
            // GamePad.SetVibration(PlayerIndex.One, 0, 0);
            Destroy(this.gameObject);
        }
    }
}
