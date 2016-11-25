using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraLockOn : MonoBehaviour
{


    //Class Scripts
    public CharacterMovement characterMovement;
    public FreeCamera cameraScript;

    public Transform selectedTarget;

    public Transform myTransform, playerT;
    public List<Transform> enemies;

    public bool isLockedOn;

    //public float maxSight;

    private int curIndex;

    private int layerMask;

    // Use this for initialization
    void Start()
    {
        layerMask = LayerMask.GetMask("Enemy");
        cameraScript = GetComponent<FreeCamera>();
        enemies = new List<Transform>();
        selectedTarget = null;

        AddAllEnemies();
    }

    private void SortByDistance()
    {
        enemies.Sort(delegate (Transform t1, Transform t2)
        {
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
            curIndex = 0;
            selectedTarget = enemies[0];

            // Raycast to hit enemy collider
            // If hits enemy, target the enemy, if not ignores the raycast
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (selectedTarget.transform.position - transform.position).normalized, out hit, layerMask))
            {
                print(hit.collider.gameObject.name);
                if (hit.collider.tag == "Enemy")
                {
                    if (selectedTarget.GetComponent<Renderer>().isVisible)
                    {
                        print("I see you");
                        //  print(hit.distance);
                        isLockedOn = true;
                        selectedTarget.GetChild(0).gameObject.SetActive(true);
                        characterMovement.enemyTarget = selectedTarget.gameObject;
                    }
                }
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
            curIndex = index;
            selectedTarget = enemies[index];

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (selectedTarget.transform.position - transform.position).normalized, out hit))
            {
                if (hit.collider.tag == "Enemy")
                {
                    isLockedOn = true;
                    selectedTarget.GetChild(0).gameObject.SetActive(true);
                    characterMovement.enemyTarget = selectedTarget.gameObject;
                }
            }
            else
            {
                playerT.eulerAngles = new Vector3(0, playerT.transform.eulerAngles.y, 0);
                characterMovement.enemyTarget = null;
            }
        }
    }

    private void ClearTarget()
    {
        selectedTarget = null;
        characterMovement.enemyTarget = null;
        isLockedOn = false;
        playerT.eulerAngles = new Vector3(0, playerT.transform.eulerAngles.y, 0); ;
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
            enemies.RemoveAt(curIndex);
        }

        selectedTarget = null;
        playerT.eulerAngles = new Vector3(0, playerT.transform.eulerAngles.y, 0);
    }

    public void notLockedOn()
    {
        selectedTarget.GetChild(0).gameObject.SetActive(false);

        curIndex = 0;
        ClearTarget();
    }


    // Update is called once per frame
    void Update()
    {

        if (enemies.Count <= 0)
            return;

        // Function to lock 
        if (Input.GetButtonDown("Target") && !selectedTarget)
        {
            if (selectedTarget) // Turning on and off enemy health UI
                selectedTarget.GetChild(0).gameObject.SetActive(false);

            TargetEnemy();
        }
        // Function to turn off lock  
        else if (Input.GetButtonDown("Target") && selectedTarget)
        {
            if (selectedTarget)  // Turning on and off enemy health UI
                notLockedOn();
        }

        // Function to switch while locked on
        if (selectedTarget)
        {
            if (Input.GetAxis("Mouse ScrollWheel") >= 1 | Input.GetAxis("Joystick X") > 0.1)
            {
                TargetEnemy();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 | Input.GetAxis("Joystick X") < -0.1)
            {
                TargetEnemy();
            }

        }

        else if (Input.GetKeyDown(KeyCode.O))
        {
            selectedTarget.GetChild(0).gameObject.SetActive(false);
            ClearTarget();
        }
    }
}
