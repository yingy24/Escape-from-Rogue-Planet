using UnityEngine;
using System.Collections;

public class CollisionEffect : MonoBehaviour {

    public GameObject collisionEffect;
	

	void OnCollisionEnter(Collision collision)
    {

        //Getting contact point from collision and instantiate collision effect
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(collisionEffect, pos, rot);

        //print("Points colliding" + collision.contacts.Length);
        //print("First normal of the point that collide: " + collision.contacts[0].normal);
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") print("Hit Enemy");
    }*/
}
