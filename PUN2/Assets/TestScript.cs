using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public Animator animator;

    private byte ANIMATION_CHANGE_EVENT = 5;

    private Transform defaultCamTransform;
    private Vector3 resetPos;
    private Quaternion resetRot;
    private GameObject cam;
    private GameObject fighter;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        defaultCamTransform = cam.transform;
        resetPos = defaultCamTransform.position;
        resetRot = defaultCamTransform.rotation;
        fighter = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
            animator.SetBool("Crouch", true);
           
        }
        else
        {
            animator.SetBool("Crouch", false);
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Jump", true);
           
        }
        else
        {
            animator.SetBool("Jump", false);
            
        }

        // Player attacks
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
