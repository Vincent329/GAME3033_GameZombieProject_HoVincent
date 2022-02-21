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

    private PlayerController playerController;
    private Rigidbody rigidbody;
    private Animator animController;
    // references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    // Animator hashes
    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");

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
        if (playerController.isJumping) return;
        if (!(inputVector.magnitude >= 0.0f))
        {
            moveDirection = Vector3.zero;
        }

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;
        currentJumpForce = playerController.isRunning ? runjumpForce : walkjumpForce;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);
        transform.position += movementDirection;

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
            rigidbody.AddForce((transform.up + moveDirection) * currentJumpForce, ForceMode.Impulse);
            animController.SetBool(isJumpingHash, playerController.isJumping);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !playerController.isJumping)
        {
            return;
        }

        playerController.isJumping = false;
        animController.SetBool(isJumpingHash, false);
    }
}
