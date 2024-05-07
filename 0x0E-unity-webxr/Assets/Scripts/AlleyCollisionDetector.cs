using UnityEngine;

public class AlleyCollisionDetector : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component controlling the animation
    private bool isInAlley = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = true;
            animator.SetBool("isInAlley", isInAlley); // Activate the isInAlley parameter in the animator
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = false;
            animator.SetBool("isInAlley", isInAlley); // Deactivate the isInAlley parameter in the animator
        }
    }
}
