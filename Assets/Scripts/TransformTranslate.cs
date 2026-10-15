using UnityEngine;

public class TransformTranslate : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float rotationSpeed = 100f;
   
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.S))
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
        }*/

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(transform.forward*movementSpeed*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(-transform.forward*movementSpeed*Time.deltaTime);
        } 

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right*movementSpeed*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right*movementSpeed*Time.deltaTime);
        } 

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.up*movementSpeed*Time.deltaTime);
        }

        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.up*movementSpeed*Time.deltaTime);
        }
        

            if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up*rotationSpeed*Time.deltaTime);
        }

        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate((-1)*transform.up*rotationSpeed*Time.deltaTime);
        } 
    }
}