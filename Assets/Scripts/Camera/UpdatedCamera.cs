using UnityEngine;
using System.Collections;

public class UpdatedCamera : MonoBehaviour
{
    public static UpdatedCamera mainCamera;

    public CameraLockOn cameraLockOn;
    public OptionMenu optionMenu;

    public Transform target;

    private Vector3 newLookAt;

    public float cameraLockedOnHeight;
    public float controllerSensitivity = 5;
    public float sensivityX = 4.0f;
    public float sensivityY = 1.0f;

    public bool useMouse;
    public bool xInvert, yInvert;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 3.5f, 0);
        public float smoothing = 100f;
        public float distanceFromTarget;
        public float zoomSmooth = 10;
        public float zoomStep = 2;
        public float maxZoom = -2;
        public float minZoom = -15;
        public bool smoothFollow = true;
        public float smooth = 0.05f;

        [HideInInspector]
        public float newDistance = -8; //set by zoom input
        [HideInInspector]
        public float adjustmentDistance = -8;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = -20;
        public float yRotation = -180;
        public float maxXRotation = 25;
        public float minXRotation = -85;
        public float vOrbitSmooth = 150;
        public float hOrbitSmooth = 150;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "Joystick Horizontal Snap";
        public string ORBIT_HORIZONTAL = "Mouse X";
        public string ORBIT_VERTICAL = "Mouse Y";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public DebugSettings debug = new DebugSettings();
    public CollisionHandler collision = new CollisionHandler();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    CharacterController charController;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput, mouseOrbitInput, vmouseOrbitInput;
    Vector3 previousMousePos = Vector3.zero;
    Vector3 currentMousePos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        cameraLockOn = GetComponent<CameraLockOn>();

        SetCameraTarget(target);

        vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = mouseOrbitInput = vmouseOrbitInput = 0;

        MoveToTarget();

        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        previousMousePos = currentMousePos = Input.mousePosition;
    }

    void SetCameraTarget(Transform t)
    {
        target = t;
        /*
        if (target != null)
        {
            if (target.GetComponent<CharacterController>())
            {
                charController = target.GetComponent<CharacterController>();
            }
            else
            {
                Debug.LogError("Camera's target needs a character controller");
            }
        }
        else
        {
            Debug.LogError("Your camera needs a target");
        }
        */
    }

    void GetInput()
    {
        /*
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

        if (!isInverted)
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
        */

        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        //hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        //zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

    void Update()
    {
        if (cameraLockOn.isLockedOn)
        {
            LockedLookAtTarget();
            return;
        }
        GetInput();
        OrbitTarget();
        //ZoominOnTarget();
    }

    void FixedUpdate()
    {
        if (cameraLockOn.isLockedOn)
        {
            //LockedLookAtTarget();
            return;
        }

        //moving
        MoveToTarget();
        //rotating
        LookAtTarget();
        //Player input orbit
        OrbitTarget();
        MouseOrbitTarget();

        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);

        //draw debug lines
        for (int i = 0; i < 5; i++)
        {
            if (debug.drawDesiredCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.desiredCameraClipPoints[i], Color.white);
            }
            if (debug.drawAdjustedCollisionLines)
            {
                Debug.DrawLine(targetPos, collision.adjustedCameraClipPoints[i], Color.green);
            }
        }

        collision.CheckColliding(targetPos); //using raycasts
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
    }

    void LockedLookAtTarget()
    {
        Vector3 dir = new Vector3(0, cameraLockedOnHeight, -5);
        Quaternion rotation = Quaternion.Euler(target.eulerAngles.x, target.eulerAngles.y, target.eulerAngles.z);
        newLookAt = new Vector3(target.position.x, target.position.y + position.targetPosOffset.y, target.position.z);
        this.transform.position = newLookAt + rotation * dir;
        transform.LookAt(newLookAt);

        Vector3 parentT = cameraLockOn.selectedTarget.parent.position;
        float dist = Vector3.Distance(target.transform.position, parentT);
        if (dist > 15)
        {
            cameraLockOn.isLockedOn = false;
            cameraLockOn.notLockedOn();
        }
    }

    void MoveToTarget()
    {
        targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;

        if (collision.colliding)
        {
            adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * Vector3.forward * position.adjustmentDistance;
            adjustedDestination += targetPos;

            if (position.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
            }
            else
                transform.position = adjustedDestination;
        }
        else
        {
            if (position.smoothFollow)
            {
                //use smooth damp function
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
            }
            else
                transform.position = destination;
        }
        //transform.position = destination;
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 100 * Time.deltaTime);
    }

    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = -180;
        }

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

        if (optionMenu.invertX.image.color == Color.red)
        {
            xInvert = true;
            print("Red");
        }
        
        else if(optionMenu.invertX.image.color != Color.red)
            xInvert = false;

        if (optionMenu.invertY.image.color == Color.red)
            yInvert = true;
        else
           yInvert = false;


        if (useMouse)
        {
            if (!xInvert && !yInvert)
            {
                orbit.xRotation += -Input.GetAxis("Mouse Y") * sensivityX * addSensitivity;
                orbit.yRotation += -Input.GetAxis("Mouse X") * sensivityY * addSensitivity;
            }
            else if (!xInvert && yInvert)
            {
                orbit.xRotation += Input.GetAxis("Mouse Y") * sensivityX * addSensitivity;
                orbit.yRotation += -Input.GetAxis("Mouse X") * -sensivityY * addSensitivity;
            }
            else if (xInvert && !yInvert)
            {
                orbit.xRotation += -Input.GetAxis("Mouse Y") * sensivityX * addSensitivity;
                orbit.yRotation += Input.GetAxis("Mouse X") * -sensivityY * addSensitivity;
            }
            else
            {
                orbit.xRotation += Input.GetAxis("Mouse Y") * sensivityX * addSensitivity;
                orbit.yRotation += Input.GetAxis("Mouse X") * -sensivityY * addSensitivity;
            }


        }

        else
        {
            if (!xInvert && !yInvert)
            {
                orbit.yRotation += -Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
                orbit.xRotation += -Input.GetAxis("Joystick Y") * sensivityY * addSensitivity;
            }
            else if (!xInvert && yInvert)
            {
                orbit.yRotation += Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
                orbit.xRotation += -Input.GetAxis("Joystick Y") * sensivityY * addSensitivity;
            }
            else if (xInvert && !yInvert)
            {
                orbit.yRotation += -Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
                orbit.xRotation += Input.GetAxis("Joystick Y") * sensivityY * addSensitivity;
            }
            else
            {
                orbit.yRotation += Input.GetAxis("Joystick X") * sensivityX * addSensitivity;
                orbit.xRotation += Input.GetAxis("Joystick Y") * sensivityY * addSensitivity;
            }           

        }

        //orbit.xRotation += vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
        //orbit.yRotation += hOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;

        CheckVerticalRotation();
    }

    void MouseOrbitTarget()
    {
        previousMousePos = currentMousePos;
        currentMousePos = Input.mousePosition;

        Vector3.Normalize(previousMousePos);
        Vector3.Normalize(currentMousePos);
        if (mouseOrbitInput > 0)
        {
            orbit.xRotation += (currentMousePos.y - previousMousePos.y) * orbit.vOrbitSmooth;
            orbit.yRotation += (currentMousePos.x - previousMousePos.x) * orbit.vOrbitSmooth;
        }

        if (vmouseOrbitInput > 0)
        {
            orbit.xRotation += (currentMousePos.y - previousMousePos.y) * (orbit.vOrbitSmooth / 2);
        }
    }

    void CheckVerticalRotation()
    {
        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }
        if (orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }
    }

    void ZoominOnTarget()
    {
        position.newDistance += position.zoomStep * zoomInput;
        position.distanceFromTarget = Mathf.Lerp(position.distanceFromTarget, position.newDistance, position.zoomSmooth * Time.deltaTime);

        if (position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }

        if (position.distanceFromTarget < position.minZoom)
        {
            position.distanceFromTarget = position.minZoom;
        }
    }

    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;
        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }

        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;
            //clear the contents of intoArray
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
            float y = x / camera.aspect;

            //top left
            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; //added and rotated the point relative to camera
            //top right
            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; //added and rotated the point relative to camera
            //bottom left
            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; //added and rotated the point relative to camera
            //top right
            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; //added and rotated the point relative to camera
            //Camera's position
            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for (int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if (Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }
            return false;
        }

        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;

            for (int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                }
            }

            if (distance == -1)
                return 0;
            else
                return distance;
        }

        public void CheckColliding(Vector3 targetPosition)
        {
            if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
    }
}
