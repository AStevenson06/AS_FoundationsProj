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
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

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

        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(movement);

            playerModel.rotation = Quaternion.Slerp(
                playerModel.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            );
        }

        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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