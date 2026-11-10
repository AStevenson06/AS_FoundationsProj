using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 3f;

    private float mouseX;
    private float mouseY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        mouseY = Mathf.Clamp(mouseY, -85f, 85f);

        // Turn the player left and right
        transform.parent.rotation = Quaternion.Euler(0, mouseX, 0);

        // Look up and down
        transform.localRotation = Quaternion.Euler(mouseY, 0, 0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
