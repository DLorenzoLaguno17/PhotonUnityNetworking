using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pause : MonoBehaviour
{
    public GameObject pausemenu;  

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausemenu.activeInHierarchy == true) 
            { 
                pausemenu.SetActive(false);
                Time.timeScale = 1;
            }
            else {

                pausemenu.SetActive(true);
                Time.timeScale = 0;
            }
            
        }

    }

    public void Resume() 
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadSceneByName("MainMenu", false);
        PhotonNetwork.LeaveRoom();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
