using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 1000f; // Adjust this value to determine how much speed boost you want to give

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the speed boost is the bowling ball
        if (other.CompareTag("Interactable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply the speed boost to the bowling ball's velocity
                rb.velocity += transform.forward * boostAmount;
            }
        }
    }
}
