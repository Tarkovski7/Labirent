using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lobbyManager : MonoBehaviour
{
    public GameObject settings,lobby;
 public void Startgame()
    {
        SceneManager.LoadScene(1);
       
    }
    public void BacktoMainMenu()
    {
        settings.SetActive(false);
        lobby.SetActive(true);
    }
    public void OpenSettings()
    {
        lobby.SetActive(false);
        settings.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }/*
    public GameObject leaderboardobj;
    public virtual void LeaderBoard(bool open)
    {
        if (leaderboardobj != null)
            leaderboardobj.SetActive(open);
    }*/
}
