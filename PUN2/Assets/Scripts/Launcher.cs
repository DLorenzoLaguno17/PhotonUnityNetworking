using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private int cnt = 0;

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
    }

    public override void OnJoinedLobby()
    {
        // Create the room or join in if it is already created
        /*if (PhotonNetwork.CountOfRooms == 0)
            PhotonNetwork.CreateRoom("FightingRoom");
        else
            PhotonNetwork.JoinRoom("FightingRoom");*/

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom("FightingRoom", options, TypedLobby.Default);
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
}
