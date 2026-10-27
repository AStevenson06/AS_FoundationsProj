using UnityEngine;

public class ClickAnim : MonoBehaviour
{
    Animator animator;

    // This script is to click to trigger an animation on the object that this script is attached to.
    void Start()
    {
        animator = GetComponent<Animator>();
    }

   void OnMouseDown()
    {
        
        animator.SetBool("isClicked", true);
    }
}
