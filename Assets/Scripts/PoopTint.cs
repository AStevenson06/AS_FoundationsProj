using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PoopEffect : MonoBehaviour
{
    public static bool isPooHigh = false;

    public float interactionDistance = 3f;
    public Image greenTint;
    public float fadeDuration = 2f;
    public float tintAlpha = 0.1f;

    private bool eaten = false;

    private void OnMouseDown()
    {
        if (eaten || isPooHigh)
            return;

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

        float distance = Vector3.Distance(
            cam.transform.position,
            transform.position
        );

        if (distance > interactionDistance)
            return;

        eaten = true;
        isPooHigh = true;

        // Hide the poo without destroying this script
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
            rend.enabled = false;

        foreach (Collider col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        // Start green screen effect
        if (greenTint != null)
            StartCoroutine(FadeGreenTint());

        // Play dialogue for current loop
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ShowDialogue(
                GetPoopDialogue()
            );
        }

        Debug.Log("POO CONSUMED!");
    }

    private IEnumerator FadeGreenTint()
    {
        greenTint.gameObject.SetActive(true);

        Color color = greenTint.color;
        color.a = 0f;
        greenTint.color = color;

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(
                0f,
                tintAlpha,
                Mathf.Clamp01(timer / fadeDuration)
            );

            greenTint.color = color;

            yield return null;
        }

        color.a = tintAlpha;
        greenTint.color = color;
    }

    private string GetPoopDialogue()
    {
        switch (LoopManager.loopCount)
        {
            case 0:
                return "Oh my god I love poo... My vision's getting weird... What was I doing again?";

            case 1:
                return "Ohhhh... My favourite snack!... Wait... Why does this taste familiar?... Have I eaten this exact poo before?...";

            case 2:
                return "Hang on... The poo... Every time I eat it, everything starts again... But surely that's just a coincidence... Right?...";

            case 3:
                return "I KNOW WHAT THIS DOES!... I KNOW IT SENDS ME BACK!... But... It's just so tasty... One more time couldn't hurt...";

            default:
                return "THE CURSE!... THE BLESSING!... THE INFINITE POO!... I KNOW WHAT AWAITS ME!... AND I CHOOSE IT AGAIN!";
        }
    }
}