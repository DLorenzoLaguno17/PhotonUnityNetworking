﻿using System.Collections;
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
    public Transform playertransform;
    private bool isDead = false;
    private bool isLoading = false;
    private float deathTime = 0.0f;

    enum Attacktype
    {
        HARD = 0,
        NORMAL,
        SOFT
    }

    private int playerHealth = 0;
    private PhotonView PV;
    private byte ATTACK_EVENT = 2;    

    private Transform defaultCamTransform;
    private Vector3 resetPos;
    private Quaternion resetRot;
    private GameObject cam;
    private GameObject fighter;
    private LevelLoader loader;
    private float timer;
    private bool blocking;
    private bool hit;
    public GameObject CrouchHitbox;
    public GameObject NormalHitbox;
    private AudioSource audsource;
    public AudioClip HitSound;
    public AudioClip MissFast;
    public AudioClip MissSlow;
    public float volume = 1f;

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
        playertransform = GetComponent<Transform>();
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
        timer = 0.0f;
        blocking = false;

        audsource = GetComponent<AudioSource>();
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
                if (!blocking)
                {
                    hit = true;
                    ReceiveDamage(damage);
                }
            }
        }
        else 
        {
            hit = false;
        }
    }

    void Update()
    {
        if (isDead)
        {
            if (Time.time - deathTime > 3.0f && !isLoading)
            {
                isLoading = true;
                loader.LoadSceneByName("PostCombat");
            }

            animator.SetBool("Punch1", false);
            animator.SetBool("Punch2", false);
            animator.SetBool("Punch3", false);

            animator.SetBool("Kick1", false);
            animator.SetBool("Kick2", false);
            animator.SetBool("Kick3", false);

            animator.SetBool("Block", false);
            animator.SetBool("Crouch", false);
            animator.SetBool("Hit", false);

            animator.SetBool("Backward", false);
            animator.SetBool("Forward", false);
           
            
            animator.SetBool("Dead", true);
        }
        else
        {
            blocking = false;

            if (timer >= 0.0)
                timer -= Time.deltaTime;

            Vector3 auxposition = transform.position;
            Quaternion auxrotation = transform.rotation;
            auxposition.x = 0.0f;
            transform.SetPositionAndRotation(auxposition, auxrotation);

            if (!PV.IsMine)
                return;

            // Player attacks
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Light  Punch"))
                {
                    if (timer <= 0.0)
                    {
                        Debug.Log("Punch");
                        LaunchAttack(attackColl[1], Attacktype.SOFT);
                        animator.SetBool("Punch3", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Light  Punch") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Punch3", false);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
                {
                    if (timer <= 0.0)
                    {
                        Debug.Log("Punch");
                        LaunchAttack(attackColl[0], Attacktype.NORMAL);
                        animator.SetBool("Punch1", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Punch1", false);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Heavy Punch"))
                {
                    if (timer <= 0.0)
                    {
                        Debug.Log("Punch");
                        LaunchAttack(attackColl[0], Attacktype.HARD);
                        animator.SetBool("Punch2", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Heavy Punch") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Punch2", false);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("LowKick"))
                {
                    if (timer <= 0.0)
                    {
                        LaunchAttack(attackColl[2], Attacktype.SOFT);
                        animator.SetBool("Kick1", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("LowKick") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Kick1", false);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Medium kick"))
                {
                    if (timer <= 0.0)
                    {
                        LaunchAttack(attackColl[1], Attacktype.NORMAL);
                        animator.SetBool("Kick3", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Medium kick") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Kick3", false);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("heavy Kcik"))
                {
                    if (timer <= 0.0)
                    {
                        Debug.Log("attack done");
                        LaunchAttack(attackColl[0], Attacktype.HARD);
                        animator.SetBool("Kick2", true);
                        timerReset();
                    }
                }
            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy Kcik") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                    animator.SetBool("Kick2", false);
            }

            // Player movement         
            if (Input.GetKey(KeyCode.D))
            {
                if (PhotonNetwork.IsMasterClient)
                    animator.SetBool("Forward", true);
                else
                    animator.SetBool("Backward", true);
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                    animator.SetBool("Forward", false);
                else
                    animator.SetBool("Backward", false);
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (PhotonNetwork.IsMasterClient)
                    animator.SetBool("Backward", true);
                else
                    animator.SetBool("Forward", true);
            }
            else
            {
                if (PhotonNetwork.IsMasterClient)
                    animator.SetBool("Backward", false);
                else
                    animator.SetBool("Forward", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                blocking = true;
                animator.SetBool("Crouch", true);
                CrouchHitbox.GetComponent<BoxCollider>().enabled = true;
                NormalHitbox.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                animator.SetBool("Crouch", false);
                CrouchHitbox.GetComponent<BoxCollider>().enabled = false;
                NormalHitbox.GetComponent<BoxCollider>().enabled = true;

            }

            //Player block
            if (Input.GetKey(KeyCode.W))
            {
                blocking = true;
                animator.SetBool("Block", true);
            }
            else
            {
                animator.SetBool("Block", false);
            }

            //Player Hit
            if (hit)
            {
                animator.SetBool("Hit", true);
            }
            else
            {
                animator.SetBool("Hit", false);
            }
        }
    }

    // Events
    private void SendCollisionEvent(int id, int damage)
    {
        object[] datas = new object[] { id, damage };
        PhotonNetwork.RaiseEvent(ATTACK_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    public void ReceiveDamage(int damage)
    {
        audsource.clip = HitSound;
        audsource.Play();

        playerHealth -= damage;
        playerBar.SetHealth(playerHealth);

        if (playerHealth <= 0 && !isDead)
        {
            isDead = true;
            deathTime = Time.time;
        }
    }

    // Attack collider handler
    private void LaunchAttack (Collider coll, Attacktype attackType)
    {
        /*audsource.clip = MissFast;
        audsource.Play();*/
       
        Collider[] cols = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, coll.transform.rotation, LayerMask.GetMask("DefenseHitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent == transform)
                continue;

            int damage = 0;
            switch(attackType)
            {
                case Attacktype.SOFT:
                   damage = 5;
                break;

                case Attacktype.NORMAL:
                    damage = 12;
                break;

                case Attacktype.HARD:
                    damage = 20;
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

    private void timerReset()
    {
        timer = 0.5f;
    }
}
