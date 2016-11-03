using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour {

    //This script was borrowed from http://wiki.unity3d.com/index.php?title=CameraFacingBillboard

    public Camera mainCamera;

    void Update()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
