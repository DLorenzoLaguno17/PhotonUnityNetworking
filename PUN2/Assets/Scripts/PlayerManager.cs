using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Vector3 spawnPos = Vector3.zero;

        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Male"), spawnPos, Quaternion.identity);
        else 
        { 
            spawnPos.z = 3.5f;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Male2"), spawnPos, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
    }
}
