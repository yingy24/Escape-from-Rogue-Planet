using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public GameObject Cam1;
    public GameObject Cam2;
    public bool camSwitch;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (Cam1.active == false)
            {
                Cam1.SetActive(true);
                Cam2.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (Cam2.active == false)
            {
                Cam1.SetActive(false);
                Cam2.SetActive(true);
            }
        }
        */
        if (camSwitch)
        {
            Cam1.SetActive(true);
            Cam2.SetActive(false);
        }

        if (!camSwitch)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
        }

        if (Input.GetButtonDown("SwitchView"))
        {
            camSwitch = !camSwitch;
            Debug.Log("Pressed SwitchView");
        }
    }
}
