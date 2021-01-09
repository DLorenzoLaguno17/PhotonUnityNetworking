using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MaleScript : MonoBehaviour
{
    public Animator animator;
    private PhotonView PV;
    private byte ANIMATION_CHANGE_EVENT = 5;

    private Transform defaultCamTransform;
    private Vector3 resetPos;
    private Quaternion resetRot;
    private GameObject cam;
    private GameObject fighter;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        defaultCamTransform = cam.transform;
        resetPos = defaultCamTransform.position;
        resetRot = defaultCamTransform.rotation;
        fighter = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == ANIMATION_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            int id = (int)datas[0];
            bool trigger = (bool)datas[1];
               
            if (id == PV.ViewID)
            {
                animator.SetBool("Forward", trigger);
            }
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Forward", true);
            object[] datas = new object[] { PV.ViewID, true };
            PhotonNetwork.RaiseEvent(ANIMATION_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
         
        }
        else {
            animator.SetBool("Forward", false);
            object[] datas = new object[] { PV.ViewID, false };
            PhotonNetwork.RaiseEvent(ANIMATION_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);

        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Crouch", true);

        }
        else
        {
            animator.SetBool("Crouch", false);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Jump", true);

        }
        else
        {
            animator.SetBool("Jump", false);
        }


        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Backward", true);
         
        }   else {
            animator.SetBool("Backward", false);
        }

       
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Punch3", true);

        }
        else
        {
            animator.SetBool("Punch3", false);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Punch1", true);

        }
        else
        {
            animator.SetBool("Punch1", false);
        }






        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Punch2", true);

        }
        else
        {
            animator.SetBool("Punch2", false);
        }


        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Kick1", true);

        }
        else
        {
            animator.SetBool("Kick1", false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Kick3", true);

        }
        else
        {
            animator.SetBool("Kick3", false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Kick2", true);

        }
        else
        {
            animator.SetBool("Kick2", false);
        }

    }
}
