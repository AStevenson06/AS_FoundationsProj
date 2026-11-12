using UnityEngine;

public class DaggerPickup : MonoBehaviour
{
    public static bool hasDagger = false;

    public float interactionDistance = 3f;

    private bool pickedUp = false;

    private void Awake()
    {
        hasDagger = false;
    }

    private void OnMouseDown()
    {
        if (pickedUp || hasDagger)
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

        // Check distance to dagger
        float distance = Vector3.Distance(
            cam.transform.position,
            transform.position
        );

        if (distance > interactionDistance)
        {
            Debug.Log("Too far away from dagger!");
            return;
        }

        // Cannot pick up dagger without potion
        if (!GrowthPotion.hasDrunkPotion)
        {
            // Fourth loop onwards: secret ending
            if (LoopManager.loopCount >= 4)
            {
                GameEnding ending =
                    FindFirstObjectByType<GameEnding>();

                if (ending != null)
                {
                    ending.StartForgotPotionEnding();
                }
                else
                {
                    Debug.LogError("GameEnding not found!");
                }

                return;
            }

            // Earlier loops: normal failed pickup dialogue
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.ShowDialogue(
                    GetFailedPickupDialogue()
                );
            }

            return;
        }

        // Find HandPosition automatically
        Transform handPosition = null;

        Transform[] allTransforms =
            player.GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allTransforms)
        {
            if (child.name == "HandPosition")
            {
                handPosition = child;
                break;
            }
        }

        if (handPosition == null)
        {
            Debug.LogError(
                "HandPosition not found under PlayerRoot!"
            );
            return;
        }

        // Successfully pick up dagger
        pickedUp = true;
        hasDagger = true;

        // Remove physics before attaching
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            Destroy(rb);
        }

        // Disable dagger collider
        Collider col = GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        // Attach dagger to hand
        transform.SetParent(handPosition);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Debug.Log("Dagger picked up!");

        // Play successful pickup dialogue
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.ShowDialogue(
                GetSuccessfulPickupDialogue()
            );
        }
    }

    // ====================================
    // FAILED PICKUP DIALOGUE
    // ====================================

    private string GetFailedPickupDialogue()
    {
        int loop = LoopManager.loopCount;
        bool pooHigh = PoopEffect.isPooHigh;

        if (pooHigh)
        {
            switch (loop)
            {
                case 0:
                    return "This spoon seems a biiiit too heavy...";

                case 1:
                    return "My favourite spoon!... Wait... Why can't I lift it?... Have we met before?";

                case 2:
                    return "Ohhhh... I remember this!... The spoon needs the magic juice!... Silly me...";

                case 3:
                    return "Spoon too heavy... Potion makes strong... Frog becomes ice cream... I've got this figured out!";

                default:
                    return "THE SPOON REJECTS ME!... I MUST PROVE MY WORTH TO THE ANCIENT POTION!";
            }
        }
        else
        {
            switch (loop)
            {
                case 0:
                    return "Damn... Too Big, I'll need to get more swole... I'm sure there's something around here... That frog could certainly use it too...";

                case 1:
                    return "Too heavy?... Wait... Didn't this happen before?... I swear I've held this dagger already...";

                case 2:
                    return "Oh come ON!... I know I need the potion first!... Why do I keep forgetting that?!";

                case 3:
                    return "Yeah yeah... Too heavy... Drink the potion... Get swole... Murder the frog... Same old routine...";

                default:
                    return "YOU FOOL!... YOU KNOW THE RULES!... THE SACRED POTION MUST BE CONSUMED BEFORE THE HOLY SPOON!";
            }
        }
    }

    // ====================================
    // SUCCESSFUL PICKUP DIALOGUE
    // ====================================

    private string GetSuccessfulPickupDialogue()
    {
        int loop = LoopManager.loopCount;
        bool pooHigh = PoopEffect.isPooHigh;

        if (pooHigh)
        {
            switch (loop)
            {
                case 0:
                    return "Yay! I can now get some spoonfuls of green ice cream!";

                case 1:
                    return "YAY! My spoon!... Wait... Didn't I already eat this ice cream?... Nahhhh...";

                case 2:
                    return "Hehehe... The green ice cream is back!... Unlimited ice cream glitch!";

                case 3:
                    return "Spoon acquired!... Frog located!... Dessert is inevitable!...";

                default:
                    return "THE HOLY SPOON RETURNS TO MY GRASP!... THE GREEN FEAST SHALL BEGIN AGAIN!";
            }
        }
        else
        {
            switch (loop)
            {
                case 0:
                    return "Mwahahah! I'm gonna get revenge!... This tubby frog won't know what hit him!";

                case 1:
                    return "Got it!... Huh... This feels strangely familiar... Like I've stabbed something with it before...";

                case 2:
                    return "Right... Dagger acquired... Now I kill the frog... Again?... Why did I say again?";

                case 3:
                    return "Dagger... Check!... Frog... Still alive!... Give me five minutes and we'll be right back here...";

                default:
                    return "THE WEAPON OF DESTINY!... THE FROG OF ETERNAL SUFFERING!... LET THE SLAUGHTER COMMENCE!";
            }
        }
    }
}