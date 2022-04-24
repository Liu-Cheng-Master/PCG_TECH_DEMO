using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    NavMeshAgent agent;

    bool foundplayer = false;
    public Transform target;
    public Transform[] wayPoints;
    int waypointnum = 0;

    float distance;


    public float maxDistance = 6;
    public float radius = 6;

    // Start is called before the first frame update
    void Start()
    {
        foundplayer = false;
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.transform.position, this.transform.position);

        if (distance < 5.0f)
        {
            foundplayer = true;
            //Debug.Log("founder: " + foundplayer);
        }

        if (foundplayer == false)
        {
            Patrol();
        }

        if (foundplayer)
        {
            agent.destination = target.position;
        }

    }

    void Patrol()
    {
        agent.destination = wayPoints[waypointnum].position;

        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            waypointnum = (waypointnum + 1) % wayPoints.Length;
        }
    }



}