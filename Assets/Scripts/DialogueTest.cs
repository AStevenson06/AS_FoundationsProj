using UnityEngine;
using System.Collections;

public class DialogueTest : MonoBehaviour
{
    private bool skipRequested = false;
    private bool openingFinished = false;

    private PlayerMovement movement;
    private Coroutine openingCoroutine;

    void Update()
    {
        if (!openingFinished &&
            (Input.GetKeyDown(KeyCode.Return) ||
             Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            skipRequested = true;
        }
    }

    IEnumerator Start()
    {
        yield return null;

        GameObject player = GameObject.Find("PlayerRoot");

        if (player == null)
        {
            Debug.LogError("PlayerRoot not found!");
            yield break;
        }

        movement = player.GetComponent<PlayerMovement>();

        if (movement == null)
        {
            Debug.LogError("PlayerMovement not found!");
            yield break;
        }

        movement.enabled = false;

        string openingDialogue = "";
        string daggerDialogue = "";

        switch (LoopManager.loopCount)
        {
            case 0:
                openingDialogue =
                    "Ugh... Where am I?... What a wild night of eating poo... Wait... Is that frog... Eating my friends?! I'm gonna kill that bastard!";

                daggerDialogue =
                    "I see a dagger on those boxes, I'll grab it!";
                break;

            case 1:
                openingDialogue =
                    "Ugh... Where am I?... Wait... This room looks familiar... Is that the same bloody frog?... Whatever... I'm gonna kill that bastard!";

                daggerDialogue =
                    "That dagger looks familiar too... Weird...";
                break;

            case 2:
                openingDialogue =
                    "Not again... That frog... That dagger... THE POO!... I've definitely done this before... Haven't I?";

                daggerDialogue =
                    "Right... Dagger first... Or was it the potion?... God, my head hurts...";
                break;

            case 3:
                openingDialogue =
                    "Oh for fuck's sake... I'm back again?... You know what?... I don't even care anymore... Where's the poo?";

                daggerDialogue =
                    "Oh yeah... The dagger... Can't forget my trusty spoon...";
                break;

            default:
                openingDialogue =
                    "THE POO CALLS TO ME... THE FROG FEARS ME... I AM INEVITABLE!";

                daggerDialogue =
                    "SPOON... POTION... FROG... POO... THE CYCLE CONTINUES!";
                break;
        }

        // Play both opening dialogues, allowing Enter to skip
        yield return PlaySkippableDialogue(openingDialogue);

        if (!skipRequested)
        {
            yield return PlaySkippableDialogue(daggerDialogue);
        }

        openingFinished = true;
        movement.enabled = true;
    }

    IEnumerator PlaySkippableDialogue(string dialogue)
    {
        if (skipRequested)
            yield break;

        bool dialogueFinished = false;

        Coroutine dialogueCoroutine = StartCoroutine(
            RunDialogue(dialogue, () => dialogueFinished = true)
        );

        while (!dialogueFinished && !skipRequested)
        {
            yield return null;
        }

        if (skipRequested)
        {
            StopCoroutine(dialogueCoroutine);

            // Hide dialogue box
            GameObject dialogueBox = GameObject.Find("DialogueBox");

            if (dialogueBox != null)
                dialogueBox.SetActive(false);
        }
    }

    IEnumerator RunDialogue(string dialogue, System.Action onComplete)
    {
        yield return DialogueManager.Instance.PlayDialogue(dialogue);
        onComplete?.Invoke();
    }
}