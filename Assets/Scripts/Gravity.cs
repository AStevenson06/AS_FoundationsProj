using UnityEngine;

public class Gravity : MonoBehaviour
{
    void OnMouseDown()
    {
        
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
