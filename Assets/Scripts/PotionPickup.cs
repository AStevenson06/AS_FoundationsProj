using UnityEngine;
using System.Collections;

public class GrowthPotion : MonoBehaviour
{
    public static bool hasDrunkPotion = false;

    public float interactionDistance = 3f;
    public float growthScale = 0.9f;

    private bool hasBeenUsed = false;

    private void Awake()
    {
        hasDrunkPotion = false;
    }

    private void OnMouseDown()
    {
        if (hasBeenUsed || hasDrunkPotion)
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

        hasBeenUsed = true;

        StartCoroutine(DrinkPotionSequence(player, cam));
    }

    private IEnumerator DrinkPotionSequence(
        GameObject player,
        Camera cam
    )
    {
        // Find PotionHand, even when disabled
        Transform potionHand = player.transform.Find(
            "Camera/PotionHand"
        );

        if (potionHand != null)
        {
            potionHand.gameObject.SetActive(true);

            Animator animator =
                potionHand.GetComponent<Animator>();

            if (animator != null)
            {
                animator.Rebind();
                animator.Update(0f);

                animator.Play("DrinkPotion", 0, 0f);

                Debug.Log("Drinking animation started!");

                // Allow Animator to update
                yield return null;

                // Get animation duration
                AnimatorStateInfo state =
                    animator.GetCurrentAnimatorStateInfo(0);

                float animationLength = state.length;

                if (animationLength <= 0f)
                    animationLength = 2f;

                // Wait until drinking animation finishes
                yield return new WaitForSeconds(animationLength);

                // Hide potion hand after drinking
                potionHand.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("PotionHand has no Animator!");
            }
        }
        else
        {
            Debug.LogError("PotionHand not found!");
        }

        // NOW grow the player
        Vector3 cameraPosition = cam.transform.position;

        player.transform.localScale =
            Vector3.one * growthScale;

        cam.transform.position = cameraPosition;

        hasDrunkPotion = true;

        Debug.Log("Potion finished! Player grown!");

        // Play dialogue AFTER drinking
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ShowDialogue(
                GetPotionDialogue()
            );
        }

        Destroy(gameObject);
    }

    private string GetPotionDialogue()
    {
        int loop = LoopManager.loopCount;
        bool pooHigh = PoopEffect.isPooHigh;

        if (pooHigh)
        {
            switch (loop)
            {
                case 0:
                    return "Is it just me or am I a little... Shorter?";

                case 1:
                    return "Ooooo... Magic juice!... Wait... Didn't I drink this yesterday?... Or tomorrow?...";

                case 2:
                    return "THE JUICE!... I remember the juice!... It makes my spoon lighter!... Or me heavier?...";

                case 3:
                    return "Mmmm... Delicious!... Same potion... Same frog... Same delicious green ice cream...";

                default:
                    return "THE SACRED NECTAR!... MY BODY EXPANDS!... MY BRAIN CONTRACTS!... PERFECTLY BALANCED!";
            }
        }
        else
        {
            switch (loop)
            {
                case 0:
                    return "Woah I'm buff now!... I can definitely grab that dagger now!";

                case 1:
                    return "Woah!... I'm swole again!... Wait... Again?... Why did I say again?";

                case 2:
                    return "Yep... Same potion... Same muscles... I knew exactly what would happen...";

                case 3:
                    return "There we go... Big again... Now for the dagger... Then the frog... Then the bloody door...";

                default:
                    return "THE POTION FLOWS THROUGH MY VEINS!... I HAVE ASCENDED BEYOND THE LIMITS OF FLYKIND!";
            }
        }
    }
}