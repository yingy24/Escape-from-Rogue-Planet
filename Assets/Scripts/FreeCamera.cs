using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour
{

    public CameraLockOn cameraLockOn;

    //public GameObject target;
    public Transform lookAt;
    private Vector3 newLookAt;
    public Transform camTransform;
    public GameObject player;
    public bool useMouse;
    

    private Camera cam;

    public float distance = 3f;
    public float offsetY = 1.8f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private string axisX;
    private string axisY;
    public float unobstructed = 0.0f;

    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;

    public float controllerSensitivity = 5;

    public float YAngleMin = 0.0f;
    public float YAngleMax = 50.0f;

    LayerMask layerMasks;

    void Start()
    {
        cameraLockOn = GetComponent<CameraLockOn>();
        // Screen.lockCursor = true;
        camTransform = transform;
        cam = Camera.main;
        currentX -= 90;

        layerMasks = 1 << LayerMask.NameToLayer("Clippable") | 0 << LayerMask.NameToLayer("NotClippable");
    }

    void Update()
    {
        if (cameraLockOn.isLockedOn)
            return;

        float addSensitivity = 0;

        if (Input.GetAxis("Mouse X") != 0 | Input.GetAxis("Mouse Y") != 0)
        {
            addSensitivity = 1;
            useMouse = true;
        }

        if (Input.GetAxis("Joystick X") != 0 |Input.GetAxis("Joystick Y") != 0)
        {
            addSensitivity = controllerSensitivity;
            useMouse = false;
        }
        
        if (useMouse)
        {
            currentX += Input.GetAxis("Mouse X") * sensivityX * addSensitivity;
            currentY += -Input.GetAxis("Mouse Y") * sensivityY * addSensitivity;
        }
        else
        {
            currentX += Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
            currentY += -Input.GetAxis("Joystick Y") * sensivityY * addSensitivity;
        }
        



        currentY = Mathf.Clamp(currentY, YAngleMin, YAngleMax);
    }

    // Update is called once per frame

    void LateUpdate()
    {

        if (cameraLockOn.isLockedOn)
        {
            Vector3 dir = new Vector3(0, 0, -5);
            Quaternion rotation = Quaternion.Euler(lookAt.eulerAngles.x, lookAt.eulerAngles.y, lookAt.eulerAngles.z);
            newLookAt = new Vector3(lookAt.position.x, lookAt.position.y + offsetY, lookAt.position.z);
            camTransform.position = newLookAt + rotation * dir;
            camTransform.LookAt(newLookAt);
        }
        else
        {
            //Central Ray
            unobstructed = distance;
            Vector3 idealPos = lookAt.TransformPoint(Vector3.forward * distance);
            RaycastHit hit;
            if (Physics.Linecast(lookAt.transform.position, idealPos, out hit, layerMasks.value))
            {
                unobstructed = hit.distance + 0.1f;
            }
            //Viewport Bleed prevention
            float c = cam.nearClipPlane;
            bool clip = true;
            while (clip)
            {
                Vector3 pos1 = cam.ViewportToWorldPoint(new Vector3(0, 0, c));
                Vector3 pos2 = cam.ViewportToWorldPoint(new Vector3(.5f, 0, c));
                Vector3 pos3 = cam.ViewportToWorldPoint(new Vector3(1, 0, c));
                Vector3 pos4 = cam.ViewportToWorldPoint(new Vector3(0, .5f, c));
                Vector3 pos5 = cam.ViewportToWorldPoint(new Vector3(1, .5f, c));
                Vector3 pos6 = cam.ViewportToWorldPoint(new Vector3(0, 1, c));
                Vector3 pos7 = cam.ViewportToWorldPoint(new Vector3(.5f, 1, c));
                Vector3 pos8 = cam.ViewportToWorldPoint(new Vector3(1, 1, c));

                if (Physics.Linecast(camTransform.position, pos1, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos2, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos3, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos4, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos5, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos6, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos7, out hit, layerMasks.value))
                {
                    // clip
                }
                else if (Physics.Linecast(camTransform.position, pos8, out hit, layerMasks.value))
                {
                    // clip
                }
                else clip = false;

                if (clip) camTransform.localPosition += camTransform.forward * c;
            }
            Vector3 dir = new Vector3(0, 0, unobstructed);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            newLookAt = new Vector3(lookAt.position.x, lookAt.position.y + offsetY, lookAt.position.z);
            camTransform.position = newLookAt + rotation * dir;
            camTransform.LookAt(newLookAt);
        }
    }
}