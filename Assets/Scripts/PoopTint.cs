using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PoopEffect : MonoBehaviour
{
public Image greenTint;

public float fadeDuration = 2f;
public float maxOpacity = 0.2f;

private void OnMouseDown()
{
StartCoroutine(TurnGreen());
}

IEnumerator TurnGreen()
{
float timer = 0f;

while (timer < fadeDuration)
{
timer += Time.deltaTime;

float alpha = Mathf.Lerp(
0f,
maxOpacity,
timer / fadeDuration
);

Color color = greenTint.color;
color.a = alpha;
greenTint.color = color;

yield return null;
}
}
}