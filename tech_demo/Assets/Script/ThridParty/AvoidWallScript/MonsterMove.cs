using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource: http://www.theappguruz.com/blog/unity-3d-enemy-obstacle-awarness-ai-code-sample

public class MonsterMove : MonoBehaviour
{
    
    private int range;
    private float speed;
    private bool iswall = false;
    public GameObject target;
    private float rotationspeed;
    private RaycastHit hit;
    private float timechanegDirec;

    void Start()
    {
        range = 10;
        speed = 1f;
        rotationspeed = 30f;
    }

    
    void Update()
    {
        // Moving towards the goal
        if (!iswall)
        {

            Vector3 relativepos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativepos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }

        // Steering
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Left and right probe lines
        Transform leftRay = transform;
        Transform rightRay = transform;

        // Turning when the left and right detection lines detect a wall
        if (Physics.Raycast(leftRay.position + (transform.right), transform.forward, out hit, 3) ||
            Physics.Raycast(leftRay.position - (transform.right), transform.forward, out hit, 3))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                iswall = true;
                transform.Rotate(Vector3.up * Time.deltaTime * rotationspeed);
            }
        }

        // No turning when the left and right detection lines detect the walls
        if (Physics.Raycast(transform.position - (transform.forward), transform.right, out hit, 3) ||
            Physics.Raycast(transform.position - (transform.forward), - transform.right, out hit, 3))
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                iswall = false;
            }
        }

        // Display of detection lines
        Debug.DrawRay(leftRay.position + (transform.right), transform.forward * range, Color.red);
        Debug.DrawRay(rightRay.position - (transform.right), transform.forward * range, Color.red);
        Debug.DrawRay(transform.position - (transform.forward), transform.right * 2, Color.yellow);
        
        Debug.DrawRay(transform.position - (transform.forward), -transform.right * 2, Color.yellow);

        // Stop when near the end
        if (Vector3.Distance(transform.position,target.transform.position) <= 2.0f)
        {
            speed = 0;
            rotationspeed = 0;
        }

    }

}
