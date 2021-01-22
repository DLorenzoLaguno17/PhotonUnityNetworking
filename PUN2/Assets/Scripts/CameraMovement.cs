using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float increaseTime = 0.005f;
    private float lastTimeIncreased = 0.0f;

    private Vector3 player1 = Vector3.zero;
    private Vector3 player2 = Vector3.zero;
    Vector3 lastPointInBetween = Vector3.zero;

    private Transform transform;
    private Vector3 pos = Vector3.zero;
    private Quaternion rot = Quaternion.identity;

    void Start()
    {
        transform = GetComponent<Transform>();
        pos = transform.position;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Get players' positions
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length >= 2)
        {
            player1 = players[0].transform.position;
            player2 = players[1].transform.position;
        }

        // Calculate camera position
        Vector3 distance = player2 - player1;
        Vector3 pointInBetween = player2 - (distance / 2);
        if (Mathf.Abs(pointInBetween.magnitude - lastPointInBetween.magnitude) > 0.5f)
            lastPointInBetween = pointInBetween;

        // Move camera
        if (Time.time - lastTimeIncreased > increaseTime && distance.magnitude < 100)
        {
            if (Mathf.Abs(pos.z - lastPointInBetween.z) > 1.0f)
            {
                if (pos.z < lastPointInBetween.z)
                    pos.z += 0.05f;
                else
                    pos.z -= 0.05f;                
            }

            transform.SetPositionAndRotation(pos, rot); 
            lastTimeIncreased = Time.time;
        }
        else
        {

        }
    }
}
