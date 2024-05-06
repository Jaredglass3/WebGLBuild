using UnityEngine;

public class BowlingBallMovement : MonoBehaviour
{
    private bool isInAlley = false;
    private float movementSpeed = 5f; // Adjust the movement speed as needed

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Alley"))
        {
            isInAlley = false;
        }
    }

    void Update()
    {
        if (isInAlley)
        {
            // Check for arrow key input or VR joystick input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Move the ball based on input
            Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            transform.Translate(movementDirection * movementSpeed * Time.deltaTime);
        }
    }
}
