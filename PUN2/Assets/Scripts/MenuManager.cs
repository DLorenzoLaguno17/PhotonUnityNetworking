using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        controlsImage = GameObject.Find("ControlsImage").GetComponent<Image>();
        creditsText = GameObject.Find("CreditsText").GetComponent<Text>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();

        playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        creditsBackButton = GameObject.Find("CreditsBackButton").GetComponent<Button>();
        controlsButton = GameObject.Find("ControlsButton").GetComponent<Button>();
        controlsBackButton = GameObject.Find("ControlsBackButton").GetComponent<Button>();
    }

    // -----------------------------
    // BUTTON FUNCTIONS
    // -----------------------------

    public void PlayButton()
    {

    }

    public void CreditsButton()
    {
        HideMenu();
    }

    public void ControlsButton()
    {
        HideMenu();
    }

    public void CreditsBackButton()
    {
        creditsBackButton.gameObject.SetActive(false);
        creditsText.gameObject.SetActive(false);

        ShowMenu();
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
        playButton.gameObject.SetActive(false); 
        creditsButton.gameObject.SetActive(false); 
        controlsButton.gameObject.SetActive(false);
    }

    private void ShowMenu()
    {
        playButton.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(true);
        controlsButton.gameObject.SetActive(true);
    }
}
