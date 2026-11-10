using UnityEngine;
using System.Collections;

public class FrogDeath : MonoBehaviour
{
    private bool dead = false;

    public void Die(float disappearDelay)
    {
        if (dead) return;

        dead = true;

        Animator animator = GetComponentInChildren<Animator>();

        if (animator != null)
            animator.enabled = false;

        StartCoroutine(FallOver(disappearDelay));
    }

    IEnumerator FallOver(float disappearDelay)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(90f, 0f, 0f);

        float duration = 0.6f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(
                startRotation,
                endRotation,
                elapsed / duration
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        yield return new WaitForSeconds(disappearDelay);

        Destroy(gameObject);
    }
}