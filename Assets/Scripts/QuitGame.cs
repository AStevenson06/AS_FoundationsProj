using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("QUIT CLICK DETECTED!");
        Quit();
    }

    public void Quit()
    {
        Debug.Log("QUITTING GAME!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}