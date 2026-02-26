using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Accessibility;

public class PlayerLocomotion : MonoBehaviour
{
    Rigidbody rb;

    
    PlayerInput playerInput;

    [SerializeField] Transform camera;
    public float cameraHeight = 1;
    Vector2 mousePosition = Vector2.zero;   
    Vector2 cameraRotation = Vector2.zero;

    InputAction jump;

    Vector2 wasdDirection = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public float jumpForce = 100;

    public float mouseSensitivity = 1;
    public float moveForce = 100;
    public float maxSpeed = 10;

    public float airDrag = 0.1f;
    public float linearDrag = 10f;
    bool isGrounded = false;
    bool groundedTimerFinished = true;

    public GameObject groundDetection;
    public LayerMask groundLayer;

    Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        //playerInput.Enable();
    }

    private void OnDisable()
    {
        //playerInput.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        wasdDirection = context.ReadValue<Vector2>();
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(groundedTimerFinished == true && isGrounded == true)
            {
                
                rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
                Invoke("GroundedTimer", 1f);
                groundedTimerFinished = false;
            }
        }
        
    }

    void GroundedTimer()
    {
        groundedTimerFinished = true;
    }

    public void MouseLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerTowardsCamera();
        GroundDetection();
        Locomotion();
    }

    private void RotatePlayerTowardsCamera()
    {
        if (mainCamera != null)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0f; // Ignore the y-axis rotation

            if (cameraForward != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(cameraForward);
                transform.rotation = newRotation;
            }
        }
    }
    void GroundDetection()
    {

        if (Physics.Raycast(groundDetection.transform.position, Vector3.down, 0.3f, groundLayer.value))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
    }


    private void Locomotion()
    {
        moveDirection = transform.right * wasdDirection.x + transform.forward * wasdDirection.y;

        if (isGrounded)
        {
            
            rb.AddForce(new Vector3(moveDirection.x * moveForce * Time.deltaTime, 0, moveDirection.z * moveForce * Time.deltaTime), ForceMode.Acceleration);
            rb.linearDamping = linearDrag;
            rb.maxLinearVelocity = maxSpeed;
        }
        else
        {
            rb.maxLinearVelocity = 9999999;
            rb.AddForce(new Vector3(moveDirection.x * (moveForce / airDrag) * Time.deltaTime, 0, moveDirection.z * (moveForce / airDrag) * Time.deltaTime), ForceMode.Acceleration);
            

            
            rb.linearDamping = 0;
        }
    }
}
