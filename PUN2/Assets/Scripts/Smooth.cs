using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : MonoBehaviourPun, IPunObservable
{
    Vector3 latestPos;
    Quaternion latestRot;
    bool valuesReceived = false;
    float currentTime = 0.0f;
    double lastPacketTime = 0;
    double currentPacketTime = 0;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();

            valuesReceived = true;
            currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Lag compensation
        double timeToReachGoal = currentPacketTime - lastPacketTime;
        currentTime += Time.deltaTime;
        float t = Mathf.Clamp((float)(currentTime / timeToReachGoal), 0f, 0.99f);

        if (!photonView.IsMine && valuesReceived)
        {
            //Update Object position and Rigidbody parameters
            transform.position = Vector3.Lerp(transform.position, latestPos, t);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, t);
        }
    }
}