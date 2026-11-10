using UnityEngine;
using System.Collections;

public class DaggerAttack : MonoBehaviour
{
public float attackRange = 10f;
public Animator daggerAnimation;

public float stabDelay = 0.2f;

private bool isStabbing = false;

void Update()
{
if (!Input.GetMouseButtonDown(0))
return;

if (!DaggerPickup.hasDagger)
return;

if (isStabbing)
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

StartCoroutine(StabFrog(frog, blood));
}
}

IEnumerator StabFrog(GameObject frog, GameObject blood)
{
isStabbing = true;

// Play dagger stabbing animation
daggerAnimation.SetTrigger("Stab");

// Wait until dagger hits frog
yield return new WaitForSeconds(stabDelay);

// Replace frog with blood
blood.transform.SetPositionAndRotation(
frog.transform.position,
frog.transform.rotation
);

blood.SetActive(true);
frog.SetActive(false);

Debug.Log("Frog stabbed and replaced with Blood!");

// Wait for animation to finish
yield return new WaitForSeconds(0.5f);

isStabbing = false;
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