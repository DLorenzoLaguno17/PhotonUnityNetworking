using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PostCombatButtons : MonoBehaviour
{
    private LevelLoader loader;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
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
        }
    }
}
