using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    // Variables
    private LevelLoader loader;
    private Image controlsImage;
    private Text creditsText;
    private Text titleText;

    private Button playButton;
    private Button creditsButton;
    private Button creditsBackButton;
    private Button controlsButton;
    private Button controlsBackButton;

    // -----------------------------
    // CORE
    // -----------------------------

    void Awake()
    {
        // Get all the references
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        controlsImage = GameObject.Find("ControlsImage").GetComponent<Image>();
        creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();

        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        creditsBackButton = GameObject.Find("CreditsBackButton").GetComponent<Button>();
        controlsButton = GameObject.Find("ControlsButton").GetComponent<Button>();
        controlsBackButton = GameObject.Find("ControlsBackButton").GetComponent<Button>();

        // Deactivate certain UI
        controlsImage.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);
        creditsBackButton.gameObject.SetActive(false);
        controlsBackButton.gameObject.SetActive(false);
    }

    // -----------------------------
    // BUTTON FUNCTIONS
    // -----------------------------

    public void PlayButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            loader.LoadNextLevel();
        }
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

        HideMenu();
    }

    public void ControlsBackButton()
    {
        controlsBackButton.gameObject.SetActive(false);
        controlsImage.gameObject.SetActive(false);

        ShowMenu();
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
    }

    private void ShowMenu()
    {
        titleText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
    }
}
