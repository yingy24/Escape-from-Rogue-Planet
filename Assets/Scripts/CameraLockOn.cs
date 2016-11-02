using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraLockOn : MonoBehaviour {


    //Class Scripts
    public CharacterMovement characterMovement;
    public FreeCamera cameraScript;

    public Transform selectedTarget;

    public Transform myTransform;
    public List <Transform> enemies;

    public bool isLockedOn;

	// Use this for initialization
	void Start () {
        cameraScript = GetComponent<FreeCamera>();
        enemies = new List<Transform>();
        selectedTarget = null;

        AddAllEnemies();
	}

    private void SortByDistance()
    {
        enemies.Sort(delegate (Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });
    }

    public void AddAllEnemies()
    {
        GameObject[] gO = GameObject.FindGameObjectsWithTag("MeleeEnemy");

        foreach(GameObject enemy in gO)
        {
            AddTarget(enemy.transform);
        }
    }

    public void AddTarget(Transform enemy)
    {
            enemies.Add(enemy);
    }

    private void TargetEnemy()
    {

        if (selectedTarget == null)
        {
            SortByDistance();
            selectedTarget = enemies[0];
            isLockedOn = true;

        }

        else
        {
            int index = enemies.IndexOf(selectedTarget);

            if (index < enemies.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
            selectedTarget = enemies[index];
            isLockedOn = true;
        }

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if(hit.collider.tag == "MeleeEnemy")
            characterMovement.target = selectedTarget.gameObject;
        }
    }


    private void ClearTarget()
    {
        selectedTarget = null;
        characterMovement.target = null;
        isLockedOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            TargetEnemy();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            ClearTarget();
        }
    }
}
