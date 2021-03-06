﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviour
{
    // Variables
    private LevelLoader loader;
    private Image controlsImage;
    private Text controlsText;
    private Text creditsText;
    private Text titleText;

    public Button playButton;
    private Button creditsButton;
    private Button creditsBackButton;
    private Button controlsButton;
    private Button controlsBackButton;
    private Button quitButton;

    // -----------------------------
    // CORE
    // -----------------------------

    void Awake()
    {
        // Get all the references
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        controlsImage = GameObject.Find("ControlsImage").GetComponent<Image>();
        controlsText = GameObject.Find("ControlsText").GetComponent<Text>();
        creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();

        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        creditsBackButton = GameObject.Find("CreditsBackButton").GetComponent<Button>();
        controlsButton = GameObject.Find("ControlsButton").GetComponent<Button>();
        controlsBackButton = GameObject.Find("ControlsBackButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        // Deactivate certain UI
        controlsImage.gameObject.SetActive(false);
        controlsText.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);
        creditsBackButton.gameObject.SetActive(false);
        controlsBackButton.gameObject.SetActive(false);
    }

    // -----------------------------
    // BUTTON FUNCTIONS
    // -----------------------------

    public void PlayButton()
    {
        GetComponent<Launcher>().EnterRoom();
        loader.LoadSceneByName("ChoosingMenu", false);
    }

    public void CreditsButton()
    {
        creditsBackButton.gameObject.SetActive(true);
        creditsText.gameObject.SetActive(true);

        HideMenu();
    }

    public void CreditsBackButton()
    {
        creditsBackButton.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);

        ShowMenu();
    }

    public void ControlsButton()
    {
        controlsBackButton.gameObject.SetActive(true);
        controlsImage.gameObject.SetActive(true);
        controlsText.gameObject.SetActive(true);

        HideMenu();
    }

    public void ControlsBackButton()
    {
        controlsBackButton.gameObject.SetActive(false);
        controlsImage.gameObject.SetActive(false);
        controlsText.gameObject.SetActive(false);

        ShowMenu();
    }

    public void Quit()
    {
        Application.Quit();

    }
    // -----------------------------
    // TOOLS
    // -----------------------------

    private void HideMenu()
    {
        titleText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false); 
        creditsButton.gameObject.SetActive(false); 
        controlsButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
    }

    private void ShowMenu()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
}
