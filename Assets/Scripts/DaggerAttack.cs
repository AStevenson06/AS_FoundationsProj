using UnityEngine;

public class DaggerAttack : MonoBehaviour
{
    public float attackRange = 10f;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!DaggerPickup.hasDagger)
            return;

        Ray ray = GetComponent<Camera>().ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            GameObject frog = hit.collider.transform.root.gameObject;

            if (frog.name != "Frog_Wizard")
                frog = hit.collider.GetComponentInParent<FrogDeath>()?.gameObject;

            if (frog == null || frog.name != "Frog_Wizard")
                return;

            GameObject blood = Resources.FindObjectsOfTypeAll<GameObject>()
                .FindBlood();

            if (blood == null)
            {
                Debug.LogError("Blood model not found!");
                return;
            }

            blood.transform.SetPositionAndRotation(
                frog.transform.position,
                frog.transform.rotation
            );

            blood.SetActive(true);
            frog.SetActive(false);

            Debug.Log("Frog replaced with Blood!");
        }
    }
}

public static class BloodFinder
{
    public static GameObject FindBlood(this GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            if (obj.name == "Blood" && obj.scene.IsValid())
                return obj;
        }

        return null;
    }
}