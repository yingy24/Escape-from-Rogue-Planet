using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour {

    public CameraLockOn cameraLockOn;

    //public GameObject target;
    public Transform lookAt;
    private Vector3 newLookAt;
    public Transform camTransform;
    public bool useMouse;

    private Camera cam;

    public float distance = 10.0f;
    public float offsetY = 1.8f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;

    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;

    public float controllerSensitivity = 5;

    public float YAngleMin = 0.0f;
    public float YAngleMax = 50.0f;

    void Start () {
        cameraLockOn = GetComponent<CameraLockOn>();
       // Screen.lockCursor = true;
        camTransform = transform;
        cam = Camera.main;
        currentX -= 90;
    }

    void Update()
    {
        if (cameraLockOn.isLockedOn)
            return;

        if (!useMouse)
        {
            currentX += Input.GetAxis("Joystick X") * sensivityX * controllerSensitivity;
            currentY += -Input.GetAxis("Joystick Y") * sensivityY * controllerSensitivity;
        }
        else
        {
            currentX += Input.GetAxis("Mouse X") * sensivityX;
            currentY += -Input.GetAxis("Mouse Y") * sensivityY;
        }

        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);
    }
    
	// Update is called once per frame

	void LateUpdate () {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            newLookAt = new Vector3(lookAt.position.x, lookAt.position.y + offsetY, lookAt.position.z);
            camTransform.position = newLookAt + rotation * dir;
            camTransform.LookAt(newLookAt);
	}
}
