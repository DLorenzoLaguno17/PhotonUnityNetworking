using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SelectionButtons : MonoBehaviour
{
    private LevelLoader loader;
    private Text secondPlayerText;
    private Image secondPlayerImage;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        secondPlayerText = GameObject.Find("TextP2").GetComponent<Text>();
        secondPlayerImage = GameObject.Find("ImageP2").GetComponent<Image>();
        secondPlayerImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            secondPlayerText.text = "Player 2";
            secondPlayerImage.gameObject.SetActive(true);
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