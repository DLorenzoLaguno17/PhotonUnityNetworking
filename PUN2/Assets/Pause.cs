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
    private byte PAUSE_GAME_EVENT = 13;
    private byte RESUME_GAME_EVENT = 37;
    private float lastTime = 0;

    private void Start()
    {
        lastTime = Time.time;
    }

    // Event handlers
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += ReceivePauseEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= ReceivePauseEvent;
    }

    private void ReceivePauseEvent(EventData obj)
    {
        if (obj.Code == LEFT_ROOM_EVENT)
            Destroy(otherPlayer);
        else if (obj.Code == RESUME_GAME_EVENT)
        {
            Time.timeScale = 1;
            pausemenu.SetActive(false);
        }
        else if (obj.Code == PAUSE_GAME_EVENT)
        {
            Time.timeScale = 0;
            pausemenu.SetActive(true);
        }
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

                object[] datas = new object[] { "mamausa" };
                PhotonNetwork.RaiseEvent(RESUME_GAME_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
            }
            else {

                pausemenu.SetActive(true);
                Time.timeScale = 0;

                object[] datas = new object[] { "minor" };
                PhotonNetwork.RaiseEvent(PAUSE_GAME_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
            }            
        }
    }

    public void Resume() 
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;

        object[] datas = new object[] { "mamausa" };
        PhotonNetwork.RaiseEvent(RESUME_GAME_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
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
