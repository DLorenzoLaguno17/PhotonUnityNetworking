using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class SelectionButtons : MonoBehaviour
{
    private LevelLoader loader;
    private Text secondPlayerText;
    private byte CONNECTION_EVENT = 10;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        secondPlayerText = GameObject.Find("Player2").GetComponent<Text>();
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += ConectionEventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= ConectionEventReceived;
    }

    private void ConectionEventReceived(EventData obj)
    {
        if (obj.Code == CONNECTION_EVENT)
        {
            secondPlayerText.text = "pito";
        }
    }

    public void PlayButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loader.LoadSceneByName("CombatScene");
        }
    }
}
