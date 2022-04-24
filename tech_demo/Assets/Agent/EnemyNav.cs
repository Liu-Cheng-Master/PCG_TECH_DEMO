using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNav : MonoBehaviour
{
    // Applying NaviMesh components for pathfinding
    NavMeshAgent agent;

    bool foundplayer = false;
    public Transform target;
    public Transform[] wayPoints;
    int waypointnum = 0;

    float distance;

    public float maxDistance = 6;
    public float radius = 6;

    
    void Start()
    {
        foundplayer = false;
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        // Calculate the distance to the player
        distance = Vector3.Distance(target.transform.position, this.transform.position);

        // Determine if a player is found
        if (distance < 5.0f)
        {
            foundplayer = true;
            //Debug.Log("founder: " + foundplayer);
        }

        // Patrol if no player found
        if (foundplayer == false)
        {
            Patrol();
        }

        // If the player is found, the player is pursued
        if (foundplayer)
        {
            agent.destination = target.position;
        }

    }

    // Waypoint method of wayfinding during patrols
    void Patrol()
    {
        agent.destination = wayPoints[waypointnum].position;

        if (agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            waypointnum = (waypointnum + 1) % wayPoints.Length;
        }
    }



}