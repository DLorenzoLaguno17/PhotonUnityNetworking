using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MaleScript : MonoBehaviour
{
    public Animator animator;
    public HealthBar playerBar;
    public Collider[] attackColl;

    enum Attacktype
    {
        HARD = 0,
        NORMAL,
        SOFT
    }

    private int playerHealth = 0;
    private PhotonView PV;
    private byte ATTACK_EVENT = 2;
    private byte ANIMATION_CHANGE_EVENT = 5;    

    private Transform defaultCamTransform;
    private Vector3 resetPos;
    private Quaternion resetRot;
    private GameObject cam;
    private GameObject fighter;
    private LevelLoader loader;

    public enum AnimationChange
    {
        Forward,
        Backward,
        Punch1,
        Punch2,
        Punch3,
        Kick1,
        Kick2,
        Kick3,
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
        loader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        // Player health
        playerHealth = (int)playerBar.slider.maxValue;
        playerBar.SetMaxHealth(playerHealth);
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
        if (obj.Code == ATTACK_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            int id = (int)datas[0];
            int damage = (int)datas[1];

            if (id == PV.ViewID)
            {
                ReceiveDamage(damage);
            }
        }
        else if (obj.Code == ANIMATION_CHANGE_EVENT)
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

                else if (anim == AnimationChange.Punch1)
                    animator.SetBool("Punch", trigger);

                else if (anim == AnimationChange.Punch2)
                    animator.SetBool("Punch", trigger);

                else if (anim == AnimationChange.Punch3)
                    animator.SetBool("Punch", trigger);

                else if (anim == AnimationChange.Kick1)
                    animator.SetBool("Kick", trigger);

                else if (anim == AnimationChange.Kick2)
                    animator.SetBool("Kick", trigger);

                else if (anim == AnimationChange.Kick3)
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
            SendAnimationEvent(true, AnimationChange.Punch3);
            LaunchAttack(attackColl[1], Attacktype.SOFT);
        }
        else
        {
            animator.SetBool("Punch3", false);
            SendAnimationEvent(false, AnimationChange.Punch3);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Punch1", true);
            SendAnimationEvent(true, AnimationChange.Punch1);
            LaunchAttack(attackColl[0], Attacktype.NORMAL);
        }
        else
        {
            animator.SetBool("Punch1", false);
            SendAnimationEvent(false, AnimationChange.Punch1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetBool("Punch2", true);
            SendAnimationEvent(true, AnimationChange.Punch2);
            LaunchAttack(attackColl[0], Attacktype.HARD);
        }
        else
        {
            animator.SetBool("Punch2", false);
            SendAnimationEvent(false, AnimationChange.Punch2);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetBool("Kick1", true);
            SendAnimationEvent(true, AnimationChange.Kick1);
            LaunchAttack(attackColl[2], Attacktype.SOFT);
        }
        else
        {
            animator.SetBool("Kick1", false);
            SendAnimationEvent(false, AnimationChange.Kick1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Kick3", true);
            SendAnimationEvent(true, AnimationChange.Kick3);
            LaunchAttack(attackColl[1], Attacktype.NORMAL);
        }
        else
        {
            animator.SetBool("Kick3", false);
            SendAnimationEvent(false, AnimationChange.Kick3);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Kick2", true);
            SendAnimationEvent(true, AnimationChange.Kick2);
            LaunchAttack(attackColl[0], Attacktype.HARD);
        }
        else
        {
            animator.SetBool("Kick2", false);
            SendAnimationEvent(false, AnimationChange.Kick2);
        }
    }

    // Events
    private void SendAnimationEvent(bool trigger, AnimationChange anim)
    {
        object[] datas = new object[] { PV.ViewID, trigger, anim };
        PhotonNetwork.RaiseEvent(ANIMATION_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    private void SendCollisionEvent(int id, int damage)
    {
        object[] datas = new object[] { id, damage };
        PhotonNetwork.RaiseEvent(ATTACK_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void ReceiveDamage(int damage)
    {
        playerHealth -= damage;
        playerBar.SetHealth(playerHealth);

        if (playerHealth <= 0)
        {
            loader.LoadSceneByName("PostCombat");
        }
    }

    // Attack collider handler
    private void LaunchAttack (Collider coll, Attacktype attackType)
    {
        Collider[] cols = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, coll.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent == transform)
                continue;

            int damage = 0;
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
            GameObject enemy = c.transform.parent.parent.gameObject;

            // Damage enemy and send event to the other client
            if (enemy)
            {
                int id = enemy.GetComponent<PhotonView>().ViewID;
                SendCollisionEvent(id, damage);
                enemy.GetComponent<MaleScript>().ReceiveDamage(damage);
            }
        }
    }
}
