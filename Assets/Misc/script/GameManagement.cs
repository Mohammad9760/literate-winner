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
    public static int minimapOpenedCounter = 0;

    private void ToggleMiniMap()
    {
        GameCamera.SetActive(!GameCamera.activeInHierarchy);
        MapCamera.SetActive(!MapCamera.activeInHierarchy);
        minimapOpenedCounter = minimapOpenedCounter + (MapCamera.activeInHierarchy? 1: 0);
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


    #region win/lose


    public GameObject gameoverScreen;
    public TMP_Text Status, PlayTime, Score, DeathCount, MapCount, KillCount;

    private void GameOverScreen(bool win)
    {
        gameoverScreen.SetActive(true);
        Time.timeScale = 0;
        Status.text = win? "برد" : "باخت";
        PlayTime.text = Timer;
        Score.text = pickup;
        DeathCount.text = selectedPlayer.deathCounter.ToString();
        MapCount.text = minimapOpenedCounter.ToString();
        // KillCount = 
    }

    public void Win() => GameOverScreen(true);

    public void lose() => GameOverScreen(false);

    public GameObject[] DeathQuestions;

    public void OnPlayerDied(int deathCount = 0)
    {
        DeathQuestions[deathCount].SetActive(true);
    }

    public void SubmitAnswer(TMP_Text text)
    {
        var answer = text.text;
    }

    public void Respawn() => selectedPlayer.Reborn();

    #endregion
    private void Update()
    {
        // if(Input.GetButtonDown("Cancel"))
        //     ToggleWorlds();

        if(Input.GetKeyDown(KeyCode.Tab))
            ToggleMiniMap();

        timerUI.text = Timer;
        pickupUI.text = pickup;
    }
   
}
