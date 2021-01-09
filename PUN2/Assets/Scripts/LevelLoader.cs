using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "PostCombat")
            StartCoroutine(LoadLevel(0));
        else
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        PhotonNetwork.LoadLevel(levelIndex);
        //SceneManager.LoadScene(levelIndex);
    }
}