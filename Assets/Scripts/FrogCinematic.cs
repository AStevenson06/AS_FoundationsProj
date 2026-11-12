using UnityEngine;
using System.Collections;

public class FrogCinematic : MonoBehaviour
{
    public Camera frogDeathView;

    public float moveDuration = 1.2f;
    public float returnDuration = 1f;

    public IEnumerator PlayCinematic(float deathDuration)
    {
        Camera playerCamera = GetComponent<Camera>();

        if (frogDeathView == null || playerCamera == null)
        {
            Debug.LogError("Cinematic cameras not assigned!");
            yield break;
        }

        Transform cinematic = frogDeathView.transform;

        // Remember where the cinematic camera should end up
        Vector3 destinationPosition = cinematic.position;
        Quaternion destinationRotation = cinematic.rotation;

        // Start at the player's viewpoint
        Vector3 startPosition = playerCamera.transform.position;
        Quaternion startRotation = playerCamera.transform.rotation;

        cinematic.SetPositionAndRotation(
            startPosition,
            startRotation
        );

        // Switch to cinematic camera
        frogDeathView.gameObject.SetActive(true);
        playerCamera.enabled = false;

        // Move towards the frog
        yield return MoveCamera(
            cinematic,
            startPosition,
            startRotation,
            destinationPosition,
            destinationRotation,
            moveDuration
        );

        // Tell DaggerAttack that the camera has arrived
        // and wait for the frog's death animation
        yield return new WaitForSeconds(deathDuration);

        // Return cinematic camera to player's viewpoint
        yield return MoveCamera(
            cinematic,
            destinationPosition,
            destinationRotation,
            playerCamera.transform.position,
            playerCamera.transform.rotation,
            returnDuration
        );

        // Restore normal camera
        playerCamera.enabled = true;
        frogDeathView.gameObject.SetActive(false);

        // Reset cinematic camera for future use
        cinematic.SetPositionAndRotation(
            destinationPosition,
            destinationRotation
        );
    }

    IEnumerator MoveCamera(
        Transform cameraTransform,
        Vector3 startPosition,
        Quaternion startRotation,
        Vector3 endPosition,
        Quaternion endRotation,
        float duration
    )
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            t = t * t * (3f - 2f * t);

            cameraTransform.position = Vector3.Lerp(
                startPosition,
                endPosition,
                t
            );

            cameraTransform.rotation = Quaternion.Slerp(
                startRotation,
                endRotation,
                t
            );

            yield return null;
        }

        cameraTransform.SetPositionAndRotation(
            endPosition,
            endRotation
        );
    }
}