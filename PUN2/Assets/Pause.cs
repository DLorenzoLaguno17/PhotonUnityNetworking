using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class Pause : MonoBehaviour
{
    public GameObject pausemenu;
    private GameObject otherPlayer;
    private byte LEFT_ROOM_EVENT = 7;
    private float lastTime = 0;

    private void Start()
    {
        lastTime = Time.time;
    }

    // Event handlers
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += LeaveSceneEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= LeaveSceneEvent;
    }

    private void LeaveSceneEvent(EventData obj)
    {
        if (obj.Code == LEFT_ROOM_EVENT)
            Destroy(otherPlayer);
    }

    void Update()
    {
        if (Time.time - lastTime > 0.5f)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length >= 2)
            {
                if (PhotonNetwork.IsMasterClient)
                    otherPlayer = players[1].gameObject;
                else
                    otherPlayer = players[0].gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausemenu.activeInHierarchy == true) 
            { 
                pausemenu.SetActive(false);
                Time.timeScale = 1;
            }
            else {

                pausemenu.SetActive(true);
                Time.timeScale = 0;
            }            
        }
    }

    public void Resume() 
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;

        object[] datas = new object[] { "minor" };
        PhotonNetwork.RaiseEvent(LEFT_ROOM_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
        
        PhotonNetwork.LeaveRoom();
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadSceneByName("MainMenu", false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
