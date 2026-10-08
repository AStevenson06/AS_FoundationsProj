using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    public GameObject clickObjToDestroy;

    void OnMouseDown()
    {
        Debug.Log("Object was clicked"); 
        //Debug.Log.Error("Object was clicked"); //this Log Error will stop play mode in the editor
        //Debug.LogWarning("Object was clicked");

        Destroy(clickObjToDestroy);


    }

}
