using UnityEngine;

public class CauldronEnding : MonoBehaviour
{
    private GameEnding gameEnding;
    private bool triggered = false;

    private void Start()
    {
        gameEnding = FindFirstObjectByType<GameEnding>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        // Only the player can trigger this ending
        if (!other.transform.IsChildOf(
            GameObject.Find("PlayerRoot").transform
        ) && other.gameObject.name != "PlayerRoot")
            return;

        triggered = true;

        if (gameEnding != null)
        {
            gameEnding.StartCauldronEnding();
        }
    }
}