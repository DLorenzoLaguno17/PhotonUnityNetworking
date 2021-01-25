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
    public Transform playertransform;
    private bool isLoading = false;
    

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
    private float timer;
    private bool blocking;


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
                if(!blocking)
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
                    animator.SetBool("Punch1", trigger);

                else if (anim == AnimationChange.Punch2)
                    animator.SetBool("Punch2", trigger);

                else if (anim == AnimationChange.Punch3)
                    animator.SetBool("Punch3", trigger);

                else if (anim == AnimationChange.Kick1)
                    animator.SetBool("Kick1", trigger);

                else if (anim == AnimationChange.Kick2)
                    animator.SetBool("Kick2", trigger);

                else if (anim == AnimationChange.Kick3)
                    animator.SetBool("Kick3", trigger);

                else if (anim == AnimationChange.Crouch)
                    animator.SetBool("Crouch", trigger);

                else if (anim == AnimationChange.Jump)
                    animator.SetBool("Jump", trigger);
            }
        }
    }

    void Update()
    {
        blocking = false;

        if (timer >= 0.0)
            timer -= Time.deltaTime;

        Vector3 auxposition = transform.position;
        Quaternion auxrotation = transform.rotation;
        auxposition.x = 0.0f;
        transform.SetPositionAndRotation(auxposition,auxrotation);

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
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("heavy Kcik") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f)
                animator.SetBool("Kick2", false);
        }

        // Player movement
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Forward", true);
        }
        else
        {
            animator.SetBool("Forward", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Backward", true);
        }
        else
        {
            animator.SetBool("Backward", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            blocking = true;
            animator.SetBool("Crouch", true);
        }
        else
        {
            animator.SetBool("Crouch", false);
        }

        //Player block
        if(Input.GetKey(KeyCode.W))
        {
            blocking = true;
            animator.SetBool("Block", true);
        }
        else
        {
            animator.SetBool("Block", false);
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
        playerHealth -= damage;
        playerBar.SetHealth(playerHealth);

        if (playerHealth <= 0 && PhotonNetwork.IsMasterClient && !isLoading)
        {
            isLoading = true;
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

    private void timerReset()
    {
        timer = 0.5f;
    }
}
