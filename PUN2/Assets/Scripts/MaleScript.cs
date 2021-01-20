using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MaleScript : MonoBehaviour
{
    enum Attacktype
    {
        HARD = 0,
        NORMAL,
        SOFT
    }

    public Animator animator;
    private PhotonView PV;
    private byte ANIMATION_CHANGE_EVENT = 5;
    

    private Transform defaultCamTransform;
    private Vector3 resetPos;
    private Quaternion resetRot;
    private GameObject cam;
    private GameObject fighter;

    public Collider[] attackColl;
    public enum AnimationChange
    {
        Forward,
        Backward,
        Punch,
        Kick,
        Jump,
        Crouch
    }

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
            AnimationChange anim = (AnimationChange)datas[2];
               
            if (id == PV.ViewID)
            {
                if (anim == AnimationChange.Forward)
                    animator.SetBool("Forward", trigger);

                else if (anim == AnimationChange.Backward)
                    animator.SetBool("Backward", trigger);

                else if (anim == AnimationChange.Punch)
                    animator.SetBool("Punch", trigger);

                else if (anim == AnimationChange.Kick)
                    animator.SetBool("Kick", trigger);

                else if (anim == AnimationChange.Crouch)
                    animator.SetBool("Crouch", trigger);

                else if (anim == AnimationChange.Jump)
                    animator.SetBool("Jump", trigger);
            }
        }
    }

    void Update()
    {
        if (!PV.IsMine)
            return;

        // Player movement
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Forward", true);
            SendAnimationEvent(true, AnimationChange.Forward);   
        }
        else 
        {
            animator.SetBool("Forward", false);
            SendAnimationEvent(false, AnimationChange.Forward);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Backward", true);
            SendAnimationEvent(true, AnimationChange.Backward);
        }
        else
        {
            animator.SetBool("Backward", false);
            SendAnimationEvent(false, AnimationChange.Backward);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Crouch", true);
            SendAnimationEvent(true, AnimationChange.Crouch);
        }
        else
        {
            animator.SetBool("Crouch", false);
            SendAnimationEvent(false, AnimationChange.Crouch);
        }
       
        // Player attacks
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool("Punch3", true);
            LaunchAttack(attackColl[1], Attacktype.SOFT);
        }
        else
        {
            animator.SetBool("Punch3", false);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Punch1", true);
            LaunchAttack(attackColl[0], Attacktype.NORMAL);
        }
        else
        {
            animator.SetBool("Punch1", false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Punch2", true);
            LaunchAttack(attackColl[0], Attacktype.HARD);
        }
        else
        {
            animator.SetBool("Punch2", false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Kick1", true);
            LaunchAttack(attackColl[2], Attacktype.SOFT);
        }
        else
        {
            animator.SetBool("Kick1", false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Kick3", true);
            LaunchAttack(attackColl[1], Attacktype.NORMAL);
        }
        else
        {
            animator.SetBool("Kick3", false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Kick2", true);
            LaunchAttack(attackColl[0], Attacktype.HARD);
        }
        else
        {
            animator.SetBool("Kick2", false);
        }

    }

    // Tools
    private void SendAnimationEvent(bool trigger, AnimationChange anim)
    {
        object[] datas = new object[] { PV.ViewID, trigger, anim };
        PhotonNetwork.RaiseEvent(ANIMATION_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    //Attack Collider Handler
    private void LaunchAttack (Collider coll, Attacktype attackType)
    {
        Collider[] cols = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, coll.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent = transform)
                continue;
            float damage = 0;
            switch(attackType)
            {
                case Attacktype.SOFT:
                   damage = 10;
                break;
                case Attacktype.NORMAL:
                    damage = 25;
                break;
                case Attacktype.HARD:
                    damage = 40;
                break;
                default:
                    damage = 0;
                break;


            }
           

            Debug.Log(c.name);
        }
    }

}
