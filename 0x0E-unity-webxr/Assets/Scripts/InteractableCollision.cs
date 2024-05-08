using UnityEngine;

public class InteractableCollision : MonoBehaviour
{
    // Reference to the animator component of the object you want to animate
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Interactable"
        if (other.CompareTag("Interactable"))
        {
            // Check if the object with the "Interactable" tag is colliding with an object with the "Animator" tag
            if (other.gameObject.CompareTag("Animator"))
            {
                Debug.Log("Interactable and Animator colliders hit!"); // Debug log for collider contact
                
                // Trigger the animation
                if (animator != null)
                {
                    animator.SetTrigger("Animator");
                    Debug.Log("Animator Triggered!"); // Debug log for animation trigger
                }
            }
        }
    }
}
