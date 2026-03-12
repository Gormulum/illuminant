using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Accessibility;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterController characterController;

    
    PlayerInput playerInput;
    Vector2 mousePosition = Vector2.zero;   
    Vector2 cameraRotation = Vector2.zero;

    InputAction jump;

    Vector2 wasdDirection = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public float jumpHeight = 10;

    public float mouseSensitivity = 1;
    public float moveSpeed = 10;
    public float gravity = -35f;

    public GameObject groundDetection;
    public LayerMask groundLayer;

    Camera mainCamera;
    Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative to keep grounded
        }

        if (context.performed)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);


        }
        
        velocity.y += gravity * Time.deltaTime;

        
        
    }


    public void MouseLook(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerTowardsCamera();
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
    


    private void Locomotion()
    {
        moveDirection = transform.right * wasdDirection.x + transform.forward * wasdDirection.y;

        characterController.Move(((moveDirection * moveSpeed) + velocity) * Time.deltaTime);

        
    }
}
