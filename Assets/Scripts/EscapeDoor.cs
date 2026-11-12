using UnityEngine;

public class EscapeDoor : MonoBehaviour
{
    public float interactionDistance = 3f;
    public Camera playerCamera;
    public GameEnding gameEnding;

    private void OnMouseDown()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera not assigned!");
            return;
        }

        float distance = Vector3.Distance(
            playerCamera.transform.position,
            transform.position
        );

        if (distance > interactionDistance)
        {
            Debug.Log("Too far away from the door!");
            return;
        }

        // Can't escape until frog is dead
        if (!DaggerAttack.frogKilled)
        {
            Debug.Log("You can't leave yet. The frog is still alive.");
            return;
        }

        if (gameEnding == null)
        {
            Debug.LogError("GameEnding not assigned!");
            return;
        }

        // Trigger ending sequence
        gameEnding.StartEnding();
    }
}