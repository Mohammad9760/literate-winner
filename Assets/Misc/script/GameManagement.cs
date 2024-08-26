using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManagement : MonoBehaviour
{

    public static GameManagement instance => GameObject.FindObjectOfType<GameManagement>();

    #region worlds
    public GameObject LightWorld, DarkWorld;
    public GameObject LightPlayer, DarkPlayer;
    public GameObject[] LightStuff, DarkStuff;

    public CinemachineVirtualCamera vCam;

    public enum Team { TeamLight, TeamDark};
    
    public Team currentTeam;
    public Team CurrentTeam
    {
        set
        {
            timerStarted = true;
            if (value == Team.TeamDark)
            {
                LightWorld.SetActive(true);
                DarkWorld.SetActive(false);
                DarkPlayer.SetActive(true);
                LightPlayer.SetActive(false);
                foreach (var stuff in LightStuff)
                    stuff.SetActive(false);
                foreach (var stuff in DarkStuff)
                    stuff.SetActive(true);
                // SetActive for the pickups and enemies as well
                vCam.Follow = DarkPlayer.transform;
            }
            else
            {
                LightWorld.SetActive(false);
                DarkWorld.SetActive(true);
                LightPlayer.SetActive(true);
                DarkPlayer.SetActive(false);
                foreach (var stuff in LightStuff)
                    stuff.SetActive(true);
                foreach (var stuff in DarkStuff)
                    stuff.SetActive(false);
                // SetActive for the pickups and enemies as well
                vCam.Follow = LightPlayer.transform;
            }
            currentTeam = value;
        }
        get { return currentTeam; }
    }

    // Start is called before the first frame update
    void ToggleWorlds() // aka SwitchPlayer
    {
        if (CurrentTeam == Team.TeamDark)
            SelectTeamLight();
        else
            SelectTeamDark();
    }

    public void SelectTeamDark() => CurrentTeam = Team.TeamDark;

    public void SelectTeamLight() => CurrentTeam = Team.TeamLight;

    private Player selectedPlayer
    {
        get
        {
            if (CurrentTeam == Team.TeamLight)
                return LightPlayer.GetComponent<Player>();
            else return DarkPlayer.GetComponent<Player>();
        }
    }
    
    #endregion
    
    #region minimap
    public GameObject GameCamera, MapCamera;

    private void ToggleMiniMap()
    {
        GameCamera.SetActive(!GameCamera.activeInHierarchy);
        MapCamera.SetActive(!MapCamera.activeInHierarchy);
    }
    #endregion


    #region timer
    public TMP_Text timerUI;
    private bool timerStarted;
    public string Timer
    {
        get
        {
            if (!timerStarted) return "- : -";
            selectedPlayer.timePlayed += Time.deltaTime;
            return (int)selectedPlayer.timePlayed/60 + ":" + (int)selectedPlayer.timePlayed%60;
        }
    }

    public TMP_Text pickupUI;
    public string pickup
    {
        get
        {
            return Pickup.total + "/" + Pickup.pickedup;
        }
    }
    #endregion


    public void Win()
    {
        print("you fucking won bitch!");
    }


    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
            ToggleWorlds();

        if(Input.GetKeyDown(KeyCode.Tab))
            ToggleMiniMap();

        timerUI.text = Timer;
        pickupUI.text = pickup;
    }
}
