using UnityEngine;

public class DaggerPickup : MonoBehaviour
{
    public static bool hasDagger = false;

    private Transform handPosition;
    private bool pickedUp = false;

    void Start()
    {
        GameObject hand = GameObject.Find("HandPosition");

        if (hand != null)
        {
            handPosition = hand.transform;
        }
        else
        {
            Debug.LogError("HandPosition not found!");
        }
    }

    void OnMouseDown()
    {
        if (pickedUp)
            return;

        if (!GrowthPotion.hasDrunkPotion)
        {
            Debug.Log("You need to drink the Growth Potion first!");
            return;
        }

        if (handPosition == null)
        {
            Debug.LogError("Cannot pick up dagger: HandPosition missing!");
            return;
        }

        pickedUp = true;
        hasDagger = true;

        transform.SetParent(handPosition);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
            Destroy(rb);

        Collider col = GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        Debug.Log("Dagger picked up!");
    }
}