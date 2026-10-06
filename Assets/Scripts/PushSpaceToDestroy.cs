using UnityEngine;
//This script is to Destroy the Game Object using the Spacebar

public class PushSpaceToDestroy : MonoBehaviour
{
   
   public GameObject gameObjectToDestroy; //This is a reference to the Game Object that we want to destroy

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //Destroy(gameObject);//This destroys the Game Object that the script is attached to
            
            //Destroy(this);//This destroys the script attached to the Game Object

            //Destroy(this.gameObject);//This destroys the Game Object that the script is attached to

            Destroy(gameObjectToDestroy);//This destroys the Game Object that we have referenced in the Inspector


        }
    }
}
