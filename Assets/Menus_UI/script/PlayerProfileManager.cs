using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    public string playerAge;
    public string playerSex;
}

public class PlayerProfileManager : MonoBehaviour
{
    public GameObject profileSetupUI; // Assign the UI GameObject in the Inspector
    public TMP_InputField nameInputField; // Assign the name InputField in the Inspector
    public TMP_InputField ageInputField; // Assign the age InputField in the Inspector
    public TMP_Dropdown sexDropdown; // Assign the sex Dropdown in the Inspector
    public Button createProfileButton; // Assign the Create Profile Button in the Inspector

    private string filePath;

    public static PlayerProfileManager instance; 


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (profileSetupUI == null) return;
        // Set the file path for the JSON file
        filePath = Path.Combine(Application.persistentDataPath, "playerProfile.json");
        profileSetupUI.SetActive(false);
        // // Check if the JSON file exists
        // if (File.Exists(filePath))
        // {
        //     profileSetupUI.SetActive(false);
        // }
        // else
        // {
        //     // Show the profile setup UI
        //     profileSetupUI.SetActive(true);
        // }

        // Add listener to the button
        createProfileButton.onClick.AddListener(OnCreateProfile);
    }

    public bool profileExists()
    {
        File.Delete(filePath);
        return File.Exists(filePath);
    }
    public void OnCreateProfile()
    {
        // Get the input values
        string playerName = nameInputField.text;
        string playerAge = ageInputField.text;
        string playerSex = sexDropdown.options[sexDropdown.value].text;

        // Check if all fields are filled
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerAge))
        {
            Debug.Log("Please fill in all fields.");
            return; // Do not create the profile if fields are empty
        }

        // Create a new player profile
        PlayerProfile profile = new PlayerProfile
        {
            playerName = playerName,
            playerAge = playerAge,
            playerSex = playerSex
        };

        // Convert the profile to JSON
        string json = JsonUtility.ToJson(profile, true);

        // Write the JSON to a file
        File.WriteAllText(filePath, json);

        // Hide the profile setup UI
        profileSetupUI.SetActive(false);


    }


}

