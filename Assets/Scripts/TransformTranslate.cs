using UnityEngine;

public class TransformTranslate : MonoBehaviour
{
   
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.S))
        {
            transform.Translate(0,0,1);
            Debug.Log("S Key Pressed");
        } 

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.Translate(0,0,-1);
            Debug.Log("W Key Pressed");
        } 

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-1,0,0);
            Debug.Log("A Key Pressed");
        } 

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(1,0,0);
            Debug.Log("D Key Pressed");
        }
    }
}