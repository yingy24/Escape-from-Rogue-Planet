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
        GameObject[] gO = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in gO)
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
            if (enemies[0].GetComponent<Renderer>().isVisible)
            {
                selectedTarget = enemies[0];
                isLockedOn = true;
                characterMovement.enemyTarget = selectedTarget.gameObject;
            }

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
            if (enemies[index].GetComponent<Renderer>().isVisible)
            {
                selectedTarget = enemies[index];
                isLockedOn = true;
                characterMovement.enemyTarget = selectedTarget.gameObject;
            }
            else
            {
                characterMovement.enemyTarget = null;
            }

        }

        
    }


    private void ClearTarget()
    {
        selectedTarget = null;
        characterMovement.enemyTarget = null;
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
