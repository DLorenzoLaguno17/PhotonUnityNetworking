using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PostCombatButtons : MonoBehaviour
{
    private LevelLoader loader;
    private Button rematch;
    private Button menu;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        menu = GameObject.Find("MenuButton").GetComponent<Button>();
        rematch = GameObject.Find("RematchButton").GetComponent<Button>();

        if (!PhotonNetwork.IsMasterClient)
        {
            menu.interactable = false;
            rematch.interactable = false;
        }
    }

    public void RematchButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loader.LoadSceneByName("CombatScene");
        }
    }

    public void MenuButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loader.LoadSceneByName("MainMenu");
            //PhotonNetwork.LeaveRoom();
        }
    }
}
