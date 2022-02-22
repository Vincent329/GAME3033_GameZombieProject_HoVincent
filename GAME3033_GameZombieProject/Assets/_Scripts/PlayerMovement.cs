using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private float walkjumpForce = 5;
    [SerializeField] private float runjumpForce = 2;
    [SerializeField] private float currentJumpForce;
    [SerializeField] private float currentSpeed;

    private PlayerController playerController;
    private Rigidbody rigidbody;
    private Animator animController;
    public GameObject followTarget;

    // references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    public float aimSensitivity = 1;

    // Animator hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingHash = Animator.StringToHash("isReloading");
    public readonly int AimVerticalHash = Animator.StringToHash("AimVertical");

    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        animController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        if (playerController.isJumping) return;
        if (!(inputVector.magnitude >= 0.0f))
        {
            moveDirection = Vector3.zero;
        }

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;
        currentJumpForce = playerController.isRunning ? runjumpForce : walkjumpForce;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);
        transform.position += movementDirection;


        // Aiming/looking

        // horizontal rotation
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);
        // vertical rotation
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }


        // if we aim up, adjust animations to have a mask that will let us properly animate the aim
        followTarget.transform.localEulerAngles = angles;

        // rotate the player rotation based on the look transform
        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
        Debug.Log(inputVector);
        animController.SetFloat(movementXHash, inputVector.x);
        animController.SetFloat(movementYHash, inputVector.y);
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        animController.SetBool(isRunningHash, playerController.isRunning);
    }

    public void OnJump(InputValue value)
    {
        if (!playerController.isJumping)
        {
            playerController.isJumping = value.isPressed;
            rigidbody.AddForce((transform.up + (moveDirection * currentSpeed/3)) * currentJumpForce, ForceMode.Impulse);
            animController.SetBool(isJumpingHash, playerController.isJumping);
        }
    }

    public void OnAim(InputValue value)
    {
        playerController.isAiming = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        // aim up, down, adjust animations to have a mask that will let us properly animate aim
    }

    public void OnFire(InputValue value)
    {
        playerController.isFiring = value.isPressed;
        animController.SetBool(isFiringHash, playerController.isFiring);
    }

    public void OnReload(InputValue value)
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping)
        {

            return;
            
        }

        playerController.isJumping = false;
        animController.SetBool(isJumpingHash, false);
        rigidbody.velocity = Vector3.zero;
    }
}
