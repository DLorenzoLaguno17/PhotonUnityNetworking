using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    public byte LOAD_SCENE_EVENT = 0;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += SceneEventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= SceneEventReceived;
    }

    // Event handlers
    private void SceneEventReceived(EventData obj)
    {
        if (obj.Code == LOAD_SCENE_EVENT) 
            transition.SetTrigger("Start");
    }

    private void SendSceneEvent(string sceneName)
    {
        object[] datas = new object[] { sceneName };
        PhotonNetwork.RaiseEvent(LOAD_SCENE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);

        /*if (sceneName == "MainMenu")
            PhotonNetwork.LeaveRoom();*/
    }

    // Scene loaders
    public void LoadSceneByName(string sceneName, bool loadForEveryone = true)
    {
        StartCoroutine(LoadLevel(sceneName, loadForEveryone));
    }

    IEnumerator LoadLevel(string sceneName, bool loadForEveryone)
    {
        // Transition 
        transition.SetTrigger("Start");

        if (loadForEveryone)
            SendSceneEvent(sceneName);

        // Wait for transition to end
        yield return new WaitForSeconds(transitionTime);

        if (loadForEveryone)
            PhotonNetwork.LoadLevel(sceneName);
        else
            SceneManager.LoadScene(sceneName);
    }
}