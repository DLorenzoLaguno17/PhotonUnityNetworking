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
    private Button fight;
    private bool waiting = true;

    private void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        fight = GameObject.Find("PlayButton").GetComponent<Button>();
        secondPlayerText = GameObject.Find("TextP2").GetComponent<Text>();
        secondPlayerImage = GameObject.Find("ImageP2").GetComponent<Image>();
        secondPlayerImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length > 1 && waiting)
        {
            secondPlayerText.text = "Player 2";
            secondPlayerImage.gameObject.SetActive(true);

            if (PhotonNetwork.IsMasterClient)
                fight.interactable = true;

            waiting = false;
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