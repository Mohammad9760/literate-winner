using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Assign these in the Inspector
    public Button playButton;
    public Button settingsButton;
    // public Button howToPlayButton;
    public Button quitButton;

    public GameObject settingsMenu;
    // public GameObject HowToPlayScreen;

    void Start()
    {
        // Add listeners to the buttons
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        // howToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    void OnPlayButtonClicked()
    {
        if (PlayerProfileManager.instance.profileExists())
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            PlayerProfileManager.instance.profileSetupUI.SetActive(true);
        }
    }

    void OnSettingsButtonClicked()
    {
        settingsMenu.SetActive(true);
    }

    // void OnHowToPlayButtonClicked()
    // {
    //     HowToPlayScreen.SetActive(true);
    // }

    void OnQuitButtonClicked()
    {
        // Quit the application
        Application.Quit();

        // If running in the editor, stop playing the scene
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
