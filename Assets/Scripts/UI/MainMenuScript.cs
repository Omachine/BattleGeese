using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void PlayGame()
    {
        // Load the game scene (replace "GameScene" with your actual game scene name)
        SceneManager.LoadScene("Comic Beginning");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
