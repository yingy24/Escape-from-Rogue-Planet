using UnityEngine;
using System.Collections;

public class UpdatedCamera : MonoBehaviour
{
    public CameraLockOn cameraLockOn;

    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private float distance = 3.0f;
    [SerializeField]
    private float height = 1.0f;
    [SerializeField]
    private float damping = 5.0f;
    [SerializeField]
    private bool smoothRotation = true;
    [SerializeField]
    private float rotationDamping = 10.0f;

    [SerializeField]
    private Vector3 targetLookAtOffset; // allows offsetting of camera lookAt, very useful for low bumper heights

    [SerializeField]
    private float bumperDistanceCheck = 2.5f; // length of bumper ray
    [SerializeField]
    private float bumperCameraHeight = 1.0f; // adjust camera height while bumping
    [SerializeField]
    private Vector3 bumperRayOffset; // allows offset of the bumper ray from target origin

    Vector3 wantedPosition;



    public Transform lookAt;
    private Vector3 newLookAt;
    public Transform camTransform;
    public GameObject player;
    public bool useMouse;
    public bool isInvert;


    private Camera cam;

    public float cameraLockedOnHeight;
    //public float distance = 3f;
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

    /// <Summary>
    /// If the target moves, the camera should child the target to allow for smoother movement. DR
    /// </Summary>
    private void Awake()
    {
        cameraLockOn = GetComponent<CameraLockOn>();
        //camera.transform.parent = target;
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

        if (Input.GetAxis("Joystick X") != 0 | Input.GetAxis("Joystick Y") != 0)
        {
            addSensitivity = controllerSensitivity;
            useMouse = false;
        }

        if (!isInvert)
        {
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
        }
        else
        {
            if (useMouse)
            {
                currentX += Input.GetAxis("Mouse X") * sensivityX * addSensitivity;
                currentY += -Input.GetAxis("Mouse Y") * -sensivityY * addSensitivity;
            }
            else
            {
                currentX += Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
                currentY += -Input.GetAxis("Joystick Y") * -sensivityY * addSensitivity;
            }
        }
    }


    private void FixedUpdate()
    {

        Movement();


        /*
        if (smoothRotation)
        {
            Quaternion wantedRotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
        }
        else
            transform.rotation = Quaternion.LookRotation(lookPosition - transform.position, target.up);
            */
    }


    public void Movement()
    {
        if (cameraLockOn.isLockedOn)
        {
            Vector3 dir = new Vector3(0, cameraLockedOnHeight, -5);
            Quaternion rotation = Quaternion.Euler(lookAt.eulerAngles.x, lookAt.eulerAngles.y, lookAt.eulerAngles.z);
            newLookAt = new Vector3(lookAt.position.x, lookAt.position.y + offsetY, lookAt.position.z);
            camTransform.position = newLookAt + rotation * dir;
            camTransform.LookAt(newLookAt);
        }

        else
        {

            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            newLookAt = new Vector3(lookAt.position.x, lookAt.position.y + offsetY, lookAt.position.z);
            camTransform.position = newLookAt + rotation * dir;
            camTransform.LookAt(newLookAt);
            CollisionCheck();
        }
    }

    public void CollisionCheck()
    {
        wantedPosition = target.TransformPoint(0, height, -distance);
        // check to see if there is anything behind the target
        RaycastHit hit;
        Vector3 back = target.transform.TransformDirection(-1 * Vector3.forward);

        // cast the bumper ray out from rear and check to see if there is anything behind
        if (Physics.Raycast(target.TransformPoint(bumperRayOffset), back, out hit, bumperDistanceCheck)
            && hit.transform != target) // ignore ray-casts that hit the user. DR
        {
            // clamp wanted position to hit position
            wantedPosition.x = hit.point.x;
            wantedPosition.z = hit.point.z;
            wantedPosition.y = Mathf.Lerp(hit.point.y + bumperCameraHeight, wantedPosition.y, Time.deltaTime * damping);
        }

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

        Vector3 lookPosition = target.TransformPoint(targetLookAtOffset);

    }
}