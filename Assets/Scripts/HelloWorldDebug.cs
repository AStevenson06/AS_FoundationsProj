using UnityEngine;
//This script is for a Debug Log Statement

public class HelloWorldDebug : MonoBehaviour
{
   

    void OnEnable()
    {
        Debug.Log("Hello World");

    }

     void OnDisable()
    {
        Debug.Log("Goodbye World");

    }
}