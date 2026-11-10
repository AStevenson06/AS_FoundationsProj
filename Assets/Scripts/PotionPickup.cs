
using UnityEngine;

public class GrowthPotion : MonoBehaviour
{
    public float pickupRange = 2f;

    public static bool hasDrunkPotion = false;

    private void OnMouseDown()
    {
        GameObject player = GameObject.Find("PlayerRoot");

        if (player == null)
        {
            Debug.LogError("PlayerRoot not found!");
            return;
        }

        Camera cam = player.GetComponentInChildren<Camera>();

        if (cam == null)
        {
            Debug.LogError("Player camera not found!");
            return;
        }

        // Check distance between camera and potion
        float distance = Vector3.Distance(
            cam.transform.position,
            transform.position
        );

        if (distance > pickupRange)
        {
            Debug.Log("You're too far away to reach the potion!");
            return;
        }

        // Remember camera position
        Vector3 cameraPosition = cam.transform.position;

        // Grow player
        player.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        // Restore camera position
        cam.transform.position = cameraPosition;

        // Allow dagger pickup
        hasDrunkPotion = true;

        Debug.Log("Player grown to 0.9 scale!");
        Debug.Log("Growth potion consumed!");

        // Remove potion
        Destroy(gameObject);
    }
}
