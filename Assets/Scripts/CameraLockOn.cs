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

    private int curIndex;

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
        GameObject[] gO = GameObject.FindGameObjectsWithTag("EnemyTarget");

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
                curIndex = 0;
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
                curIndex = index;
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
	
    // Remove destroyed enemy from list
    public void SwapAndDelete()
    {
        if (curIndex == enemies.Count - 1)
                   enemies.RemoveAt(curIndex);

        else
        {
            Transform tmpT = selectedTarget;
            enemies[curIndex] = enemies[enemies.Count - 1];
            //  enemies[enemies.Count - 1] = selectedTarget;
            enemies.RemoveAt(curIndex);
        }

        selectedTarget = null;
    }

	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Target"))
        {
            TargetEnemy();
            print("press target");
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            ClearTarget();
        }
    }
}
