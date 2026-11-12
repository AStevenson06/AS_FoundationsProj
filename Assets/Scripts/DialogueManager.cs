using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    public float typingSpeed = 0.035f;
    public float displayDuration = 1.5f;
    public float pauseBetweenLines = 0.5f;

    private Coroutine dialogueCoroutine;

    void Awake()
    {
        Instance = this;

        if (dialogueText != null)
            dialogueText.text = "";

        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    public void ShowDialogue(string message)
    {
        if (dialogueCoroutine != null)
            StopCoroutine(dialogueCoroutine);

        dialogueCoroutine = StartCoroutine(
            PlayDialogue(message)
        );
    }

    public void StopDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        if (dialogueText != null)
            dialogueText.text = "";

        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    public IEnumerator PlayDialogue(string message)
    {
        string[] lines = message.Split(
            new string[] { "..." },
            System.StringSplitOptions.None
        );

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (i < lines.Length - 1)
                line += "...";

            if (string.IsNullOrWhiteSpace(line))
                continue;

            dialogueBox.SetActive(true);
            dialogueText.text = "";

            foreach (char letter in line)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitForSeconds(displayDuration);

            dialogueText.text = "";
            dialogueBox.SetActive(false);

            yield return new WaitForSeconds(pauseBetweenLines);
        }

        dialogueCoroutine = null;
    }
}