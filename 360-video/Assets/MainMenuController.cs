using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private static MainMenuController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
     void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Call a method to handle entering the main menu
            EnterMainMenu();
        }
    }
    private void EnterMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LoadScene1()
    {
        SceneManager.LoadScene("Cantina");
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("Cube");
    }


    public void LoadScene3()
    {
        SceneManager.LoadScene("Living Room");
    }
}