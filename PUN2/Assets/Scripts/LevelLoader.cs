using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    public void LoadSceneByName(string sceneName, bool loadForEveryone = true)
    {
        StartCoroutine(LoadLevel(sceneName, loadForEveryone));
    }

    IEnumerator LoadLevel(string sceneName, bool loadForEveryone)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        if (loadForEveryone)
            PhotonNetwork.LoadLevel(sceneName);
        else
            SceneManager.LoadScene(sceneName);
    }
}