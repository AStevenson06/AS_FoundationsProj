using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameEnding : MonoBehaviour
{
    public GameObject blackScreen;
    public Image blackImage;
    public GameObject finImage;
    public GameObject quitButton;

    public float fadeDuration = 2f;
    public float pauseBeforeFin = 1f;

    private bool endingStarted = false;

    public void StartEnding()
    {
        if (endingStarted)
            return;

        endingStarted = true;
        StartCoroutine(EndingSequence());
    }

    IEnumerator EndingSequence()
    {
        // Show black screen and hide Fin
        blackScreen.SetActive(true);
        finImage.SetActive(false);
        quitButton.SetActive(false);

        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        // Fade to black
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Clamp01(timer / fadeDuration);
            blackImage.color = color;

            yield return null;
        }

        color.a = 1f;
        blackImage.color = color;

        // Play ending dialogue
        if (DialogueManager.Instance != null)
        {
            yield return DialogueManager.Instance.PlayDialogue(
                GetEndingDialogue()
            );
        }

        yield return new WaitForSeconds(pauseBeforeFin);

        if (PoopEffect.isPooHigh)
        {
            // POO ENDING - THE LOOP CONTINUES
            LoopManager.AddLoop();

            // Reset static gameplay variables
            PoopEffect.isPooHigh = false;
            GrowthPotion.hasDrunkPotion = false;
            DaggerPickup.hasDagger = false;
            DaggerAttack.frogKilled = false;

            // Restart the current scene
            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }

        else
        {
            // TRUE ENDING - THE FLY ESCAPES

            // Disable dialogue canvas so it can't block clicks
            GameObject dialogueCanvas = GameObject.Find("DialogueCanvas");

            if (dialogueCanvas != null)
            {
                dialogueCanvas.SetActive(false);
            }

            // Show Fin and Quit button
            finImage.SetActive(true);
            quitButton.SetActive(true);

            // Unlock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Unlock the mouse cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable player camera movement
            GameObject player = GameObject.Find("PlayerRoot");

            if (player != null)
            {
                foreach (MonoBehaviour script in
                         player.GetComponentsInChildren<MonoBehaviour>())
                {
                    if (script.GetType().Name == "MouseLook")
                        script.enabled = false;
                }
            }

            Debug.Log("TRUE ENDING! THE FLY IS FREE!");
        }

    }
    public void StartCauldronEnding()
    {
        if (endingStarted)
            return;

        endingStarted = true;
        StartCoroutine(CauldronEndingSequence());
    }

    private IEnumerator CauldronEndingSequence()
    {
        blackScreen.SetActive(true);
        finImage.SetActive(false);
        quitButton.SetActive(false);

        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        // Dramatic fade to black
        float timer = 0f;

        while (timer < 3f)
        {
            timer += Time.deltaTime;

            color.a = Mathf.Clamp01(timer / 3f);
            blackImage.color = color;

            yield return null;
        }

        color.a = 1f;
        blackImage.color = color;

        yield return new WaitForSeconds(1f);

        // Final words
        if (DialogueManager.Instance != null)
        {
            yield return DialogueManager.Instance.PlayDialogue(
                "Why would you jump in there?..."
            );
        }

        yield return new WaitForSeconds(1.5f);

        // Disable dialogue UI after the text finishes
        GameObject dialogueCanvas = GameObject.Find("DialogueCanvas");

        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        finImage.SetActive(true);
        quitButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable mouse-look
        GameObject player = GameObject.Find("PlayerRoot");

        if (player != null)
        {
            foreach (MonoBehaviour script in
                     player.GetComponentsInChildren<MonoBehaviour>())
            {
                if (script.GetType().Name == "MouseLook")
                    script.enabled = false;
            }
        }

        Debug.Log("CAULDRON ENDING!");
    }
    public void StartForgotPotionEnding()
    {
        if (endingStarted)
            return;

        endingStarted = true;
        StartCoroutine(ForgotPotionEndingSequence());
    }

    private IEnumerator ForgotPotionEndingSequence()
    {
        blackScreen.SetActive(true);
        finImage.SetActive(false);
        quitButton.SetActive(false);

        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        float timer = 0f;

        while (timer < 2f)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / 2f);
            blackImage.color = color;
            yield return null;
        }

        color.a = 1f;
        blackImage.color = color;

        if (DialogueManager.Instance != null)
        {
            yield return DialogueManager.Instance.PlayDialogue(
                "This is your fault for forgetting the potion AGAIN..."
            );
        }

        yield return new WaitForSeconds(1f);

        GameObject dialogueCanvas = GameObject.Find("DialogueCanvas");

        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);

        finImage.SetActive(true);
        quitButton.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject player = GameObject.Find("PlayerRoot");

        if (player != null)
        {
            foreach (MonoBehaviour script in
                     player.GetComponentsInChildren<MonoBehaviour>())
            {
                if (script.GetType().Name == "MouseLook")
                    script.enabled = false;
            }
        }

        Debug.Log("FORGOT POTION ENDING!");
    }

    private string GetEndingDialogue()
    {
        int loop = LoopManager.loopCount;

        if (PoopEffect.isPooHigh)
        {
            switch (loop)
            {
                case 0:
                    return "I have no idea where I am going... Is that a poo store? I'll be damned...";

                case 1:
                    return "Finally free!... Wait... That poo store looks familiar... Haven't I seen that before?...";

                case 2:
                    return "No... No no no... I remember this!... Every time I eat the poo, I end up back there!... Please... Not again...";

                case 3:
                    return "Here we go again... Back to the same room... Same frog... Same poo... I wonder how many times I've done this...";

                default:
                    return "THE CYCLE IS ETERNAL!... THE POO IS INFINITE!... THE FROG SHALL NEVER KNOW PEACE!... SEE YOU AGAIN, OLD FRIEND!";
            }
        }
        else
        {
            switch (loop)
            {
                case 0:
                    return "I escaped... I'm swole... I'm free... but I feel like I was missing something, a kick... I'm gonna go find some poo...";

                case 1:
                    return "I'm free!... Wait... This feels different... I didn't eat the poo this time... Maybe that's why...";

                case 2:
                    return "The door... The outside world... I'm actually leaving!... No more frog... No more poo... No more loops...";

                case 3:
                    return "I did it... I finally broke the cycle... All I had to do was stop eating poo... Why was that so difficult?...";

                default:
                    return "IT'S OVER!... THE CURSE IS BROKEN!... THE FROG IS DEAD!... AND I SHALL NEVER EAT POO AGAIN!... Probably...";
            }
        }
    }
}