using UnityEngine;

public class SpinButton : MonoBehaviour
{
    private bool isSpinning = false;
    private float totalRotation = 0f;
    public float spinSpeed = 180f; // Adjust this value to control rotation speed

    // Update is called once per frame
    void Update()
    {
        if (isSpinning)
        {
            // Rotate the button
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
            totalRotation += spinSpeed * Time.deltaTime;

            // Check if one full rotation is completed
            if (totalRotation >= 360f)
            {
                isSpinning = false;
                totalRotation = 0f;
            }
        }
    }

    // Call this method to start spinning the button
    public void StartSpinning()
    {
        isSpinning = true;
    }
}
