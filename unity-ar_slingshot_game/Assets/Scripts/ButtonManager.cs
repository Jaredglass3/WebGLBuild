using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject startCanvas; // Reference to the canvas containing the start button
    public GameObject gameCanvas; // Reference to the canvas containing the game UI

    public TargetController targetController; // Reference to the TargetController script

    // Method to handle the Start button click
    public void StartGame()
    {
        // Disable the start canvas
        startCanvas.SetActive(false);

        // Enable the game canvas
        gameCanvas.SetActive(true);

        // Call StartGame method from TargetController
        if(targetController != null)
        {
            targetController.StartGame();
        }
        else
        {
            Debug.LogWarning("TargetController reference not set in ButtonManager.");
        }
    }

    // Method to handle the Quit button click
    public void QuitGame()
    {
        // This will quit the application when running in standalone build
        // It won't work in the Unity Editor
        Application.Quit();
    }

    // Method to handle the Restart button click
    public void RestartGame()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
