using UnityEngine;
using System.Collections;

public class CameraClip : MonoBehaviour {

    public CameraLockOn cameraLockOn;

    public Camera myCamera;
    public Transform camTransform;
    public Transform pivot;
    public Transform character;

    public int charRotationSpeed = 4;
    public int camRotateSpeed = 10;
    public int vertSpeed = 3;
    public bool reverseVertical, isLockedOnm, useMouse;

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;
    public float controllerSensitivity = 5;
    
    float offset = -3;
    float farthestZoom = -7;
    float closestZoom = -2;
    float camFollow = 8;
    float camZoom = 1.75f;

    LayerMask mask;

    // Use this for initialization
    void Start()
    {
        // pivot = transform;
        myCamera = Camera.main;
        camTransform = myCamera.transform;
       // character = transform.parent.transform;
        camTransform.position = pivot.TransformPoint(Vector3.forward * offset);
        mask = 1 << LayerMask.NameToLayer("Clippable") | 0 << LayerMask.NameToLayer("NotClippable");
    }


    void Update()
    {

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

        //Clamp Zoom
        if (offset > closestZoom) offset = closestZoom;
        else if (offset < farthestZoom) offset = farthestZoom;

        //Central Ray
        float unobstructed = offset;
        Vector3 idealPostion = pivot.TransformPoint(Vector3.forward * offset);

        RaycastHit hit;
        if (Physics.Linecast(pivot.position, idealPostion, out hit, mask.value))
        {
            unobstructed = -hit.distance + .01f;
        }


        //smooth
        Vector3 desiredPos = pivot.TransformPoint(Vector3.forward * unobstructed);
        Vector3 currentPos = camTransform.position;

        Vector3 goToPos = new Vector3(Mathf.Lerp(currentPos.x, desiredPos.x, camFollow), Mathf.Lerp(currentPos.y, desiredPos.y, camFollow), Mathf.Lerp(currentPos.z, desiredPos.z, camFollow));

        camTransform.localPosition = goToPos;
        camTransform.LookAt(pivot.position);


        if (cameraLockOn.isLockedOn)
        {
            Vector3 dir = new Vector3(0, 0, offset);
            Vector3 newPivot;
            Quaternion rotation = Quaternion.Euler(pivot.eulerAngles.x, pivot.eulerAngles.y, pivot.eulerAngles.z);
            newPivot = new Vector3(pivot.position.x, pivot.position.y + unobstructed, pivot.position.z);
            camTransform.position = newPivot + rotation * dir;
            // camTransform.eulerAngles = lookAt.localEulerAngles;
            camTransform.LookAt(pivot);
        }
        else
        {
            Vector3 dir = new Vector3(0, 0, offset);
            Vector3 newPivot;
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            newPivot = new Vector3(pivot.position.x, pivot.position.y + unobstructed, pivot.position.z);
            // CompensateForWalls(this.transform.position, ref newLookAt);
            camTransform.position = newPivot + rotation * dir;
            camTransform.LookAt(newPivot);
        }


    // if (Input.GetButtonDown("ResetCamera")) pivot.localRotation = Quaternion.identity;


    //Viewport Bleed prevention
    float c = myCamera.nearClipPlane;
        bool clip = true;
        while (clip)
        {
            Vector3 pos1 = myCamera.ViewportToWorldPoint(new Vector3(0, 0, c));
            Vector3 pos2 = myCamera.ViewportToWorldPoint(new Vector3(.5f, 0, c));
            Vector3 pos3 = myCamera.ViewportToWorldPoint(new Vector3(1, 0, c));
            Vector3 pos4 = myCamera.ViewportToWorldPoint(new Vector3(0, .5f, c));
            Vector3 pos5 = myCamera.ViewportToWorldPoint(new Vector3(1, .5f, c));
            Vector3 pos6 = myCamera.ViewportToWorldPoint(new Vector3(0, 1, c));
            Vector3 pos7 = myCamera.ViewportToWorldPoint(new Vector3(.5f, 1, c));
            Vector3 pos8 = myCamera.ViewportToWorldPoint(new Vector3(1, 1, c));

            Debug.DrawLine(camTransform.position, pos1, Color.yellow);
            Debug.DrawLine(camTransform.position, pos2, Color.yellow);
            Debug.DrawLine(camTransform.position, pos3, Color.yellow);
            Debug.DrawLine(camTransform.position, pos4, Color.yellow);
            Debug.DrawLine(camTransform.position, pos5, Color.yellow);
            Debug.DrawLine(camTransform.position, pos6, Color.yellow);
            Debug.DrawLine(camTransform.position, pos7, Color.yellow);
            Debug.DrawLine(camTransform.position, pos8, Color.yellow);

            if (Physics.Linecast(camTransform.position, pos1, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos2, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos3, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos4, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos5, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos6, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos7, out hit, mask.value))
            {
                // clip
            }
            else if (Physics.Linecast(camTransform.position, pos8, out hit, mask.value))
            {
                // clip
            }
            else clip = false;

            if (clip) camTransform.localPosition += camTransform.forward * c;
        }
    }



}
