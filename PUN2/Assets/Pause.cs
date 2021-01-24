using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pausemenu;
  

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausemenu.activeInHierarchy == true) { 
                pausemenu.SetActive(false);
                Time.timeScale = 1;
            }
            else {

                pausemenu.SetActive(true);
                Time.timeScale = 0;
            }
            
        }

    }



    public void Resume() {
        pausemenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Mainmenu()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadSceneByName("MainMenu");


    }


    public void Quit()
    {
        Application.Quit();

    }
}
