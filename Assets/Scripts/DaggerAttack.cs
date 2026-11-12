using UnityEngine;
using System.Collections;

public class DaggerAttack : MonoBehaviour
{
    public static bool frogKilled = false;

    public float attackRange = 10f;

    public Animator daggerAnimation;
    public Animator frogAnimator;
    public FrogCinematic frogCinematic;

    public float stabDuration = 0.6f;
    public float deathDuration = 1.5f;

    // Poo-high settings
    public float frogDisappearDelay = 3f;
    public float frogShrinkDuration = 2f;
    public float pooHighGrowthScale = 1.5f;

    private bool attacking = false;

    void Awake()
    {
        frogKilled = false;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!DaggerPickup.hasDagger || attacking || frogKilled)
            return;

        Camera cam = GetComponent<Camera>();

        if (cam == null)
            return;

        Ray ray = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            Transform target = hit.collider.transform;

            while (target != null)
            {
                if (target.name.StartsWith("Wizard_Frog"))
                {
                    StartCoroutine(StabSequence());
                    return;
                }

                target = target.parent;
            }
        }
    }

    IEnumerator StabSequence()
    {
        attacking = true;

        if (daggerAnimation == null ||
            frogAnimator == null ||
            frogCinematic == null)
        {
            Debug.LogError(
                "Missing dagger or frog cinematic reference!"
            );

            attacking = false;
            yield break;
        }

        // Play stabbing animation
        daggerAnimation.SetTrigger("Stab");

        yield return new WaitForSeconds(stabDuration);

        // Play frog death cinematic
        yield return StartCoroutine(MoveToFrogAndPlayDeath());

        Debug.Log("Frog defeated! Escape door unlocked!");

        // Get dialogue based on loop and poo state
        string dialogue = GetFrogDeathDialogue();

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ShowDialogue(dialogue);
        }

        // Eat frog if poo-high
        if (PoopEffect.isPooHigh)
        {
            StartCoroutine(EatFrog());
        }

        attacking = false;
    }

    IEnumerator MoveToFrogAndPlayDeath()
    {
        // Start cinematic camera movement
        StartCoroutine(
            frogCinematic.PlayCinematic(deathDuration)
        );

        // Wait until camera reaches frog
        yield return new WaitForSeconds(
            frogCinematic.moveDuration
        );

        // Play frog death animation
        frogAnimator.SetTrigger("Die");

        frogKilled = true;

        // Wait for death animation and camera return
        yield return new WaitForSeconds(
            deathDuration + frogCinematic.returnDuration
        );
    }

    IEnumerator EatFrog()
    {
        // Wait before eating the frog
        yield return new WaitForSeconds(frogDisappearDelay);

        Transform frog = frogAnimator.transform;

        Vector3 originalScale = frog.localScale;

        float timer = 0f;

        // Gradually shrink the frog
        while (timer < frogShrinkDuration)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(
                timer / frogShrinkDuration
            );

            frog.localScale = Vector3.Lerp(
                originalScale,
                Vector3.zero,
                progress
            );

            yield return null;
        }

        // Frog completely disappears
        frog.localScale = Vector3.zero;
        frog.gameObject.SetActive(false);

        Debug.Log("Frog eaten! Delicious green ice cream!");

        // Grow player after eating frog
        GameObject player = GameObject.Find("PlayerRoot");

        if (player != null)
        {
            Camera cam = GetComponent<Camera>();

            Vector3 cameraPosition = Vector3.zero;

            if (cam != null)
                cameraPosition = cam.transform.position;

            player.transform.localScale =
                Vector3.one * pooHighGrowthScale;

            // Preserve camera world position
            if (cam != null)
                cam.transform.position = cameraPosition;

            Debug.Log("Player grew to 1.5 scale!");
        }
    }

    // =====================================
    // FROG DEATH DIALOGUE
    // =====================================

    private string GetFrogDeathDialogue()
    {
        int loop = LoopManager.loopCount;
        bool pooHigh = PoopEffect.isPooHigh;

        if (pooHigh)
        {
            switch (loop)
            {
                case 0:
                    return "YUMMY!... Wait... Why was my ice cream screaming?... Five second rule still counts";

                case 1:
                    return "YUMMY!... Wait... This ice cream tastes familiar... Didn't I eat this frog before?... Nah... Must be the sprinkles...";

                case 2:
                    return "OH GOD!... IT'S HAPPENING AGAIN!... I'M EATING THE FROG AGAIN!... Why does it taste so good though?...";

                case 3:
                    return "Another day... Another delicious frog... Another trip back to the beginning... I'm starting to think this ice cream is cursed...";

                default:
                    return "THE ETERNAL FEAST!... THE FROG SHALL BE CONSUMED!... THE CYCLE SHALL CONTINUE!... AND I SHALL GROW EVER STRONGER!";
            }
        }
        else
        {
            switch (loop)
            {
                case 0:
                    return "MWAHAHAHAHA! DIE! DIE!... Damn... Guess I better get outta here before the frog police arrive.";

                case 1:
                    return "MWAHAHAHA!... Wait... Haven't I killed this frog before?... No... That's ridiculous... Right?...";

                case 2:
                    return "THERE!... DEAD AGAIN!... AGAIN?!... Why do I keep saying that?... Something is seriously wrong here...";

                case 3:
                    return "Right... Frog dead... That's the fourth time now... I wonder if the frog remembers any of this...";

                default:
                    return "THE FROG HAS FALLEN ONCE MORE!... DEATH IS TEMPORARY!... BUT MY HATRED IS ETERNAL!...";
            }
        }
    }
}