using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public float rotationSpeed = 10f;

    public Transform cameraTransform;
    public Transform playerModel;

    private Rigidbody rb;
    private Vector3 movement;
    private bool jumpRequested;
    private bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Prevent physics from tipping the player over
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movement = (forward * z + right * x).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(
            movement.x * moveSpeed,
            rb.linearVelocity.y,
            movement.z * moveSpeed
        );

        // Make the player model face the camera's direction
        if (playerModel != null && cameraTransform != null)
        {
            Vector3 lookDirection = cameraTransform.forward;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation =
                    Quaternion.LookRotation(lookDirection);

                playerModel.rotation = Quaternion.Slerp(
                    playerModel.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                );
            }
        }

        if (jumpRequested)
        {
            rb.AddForce(
                Vector3.up * jumpForce,
                ForceMode.Impulse
            );

            jumpRequested = false;
            grounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}