using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private int cnt = 0;
    private MenuManager menu;
    private Text connecting;

    private void Awake()
    {
        menu = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        connecting = GameObject.Find("Connecting").GetComponent<Text>();

        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();

            menu.playButton.interactable = true;
            connecting.text = "CONNECTED";
        }
    }

    private void Start()
    {
        print("Connecting to server");
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.ConnectUsingSettings();
        gameObject.AddComponent<PhotonView>();
    }

    // PUN2 Methods
    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        PhotonNetwork.AutomaticallySyncScene = true;
        menu.playButton.interactable = true;
        connecting.text = "CONNECTED";
    }

    public override void OnJoinedRoom()
    {
        print("Joined room");

        print(PhotonNetwork.PlayerList.Length);
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Error creating the room: " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }

    /*public override void OnMasterClientSwitched(Player newMasterClient)
    {
        
    }*/

    // Functions for public use
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void EnterRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        options.PlayerTtl = 10000000;
        PhotonNetwork.JoinOrCreateRoom("FightingRoom", options, TypedLobby.Default);
    }
}
