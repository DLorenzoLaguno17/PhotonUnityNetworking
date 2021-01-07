using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleScript : MonoBehaviour
{
    public Animator animator;

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
        else {
            animator.SetBool("Forward", false);

        }


       if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Backward", true);
         
        }   else {
            animator.SetBool("Backward", false);
        }

        if (Input.GetKey(KeyCode.P))
        {
            animator.SetBool("Punch1", true);

        }
        else
        {
            animator.SetBool("Punch1", false);
        }

        if (Input.GetKey(KeyCode.O))
        {
            animator.SetBool("Kick1", true);

        }
        else
        {
            animator.SetBool("Kick1", false);
        }

    }
}
