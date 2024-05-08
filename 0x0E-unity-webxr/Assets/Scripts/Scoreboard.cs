using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    private HashSet<GameObject> countedPins = new HashSet<GameObject>();

    private void FixedUpdate()
    {
        // Find all GameObjects tagged as "Pin"
        GameObject[] pins = GameObject.FindGameObjectsWithTag("pins");

        foreach (GameObject pin in pins)
        {
            // Check if the pin has moved since the last frame
            if (pin.GetComponent<Rigidbody>().velocity.magnitude > 0.1f && !countedPins.Contains(pin))
            {
                // Increment the score
                score++;
                // Update the scoreboard text
                scoreText.text = "" + score;
                // Add the pin to the set of counted pins
                countedPins.Add(pin);
            }
        }
    }
}
