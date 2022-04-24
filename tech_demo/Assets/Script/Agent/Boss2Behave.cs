using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The two states of Boss
public enum BossStates { Guard,Attack}
public class Boss2Behave : MonoBehaviour
{
    private MeshRenderer msRender;
    private float smooth = 1.0f;
    public Transform boss;
    float distance;
    private BossStates bossstates;

    // Putting players who come into view into a list
    public List<GameObject> player = new List<GameObject>(); 

    void Start()
    {
        msRender = GetComponent<MeshRenderer>();
        msRender.material.color = Color.blue;
    }

    // Place players in the list when the collision box detects them
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            player.Add(col.gameObject);
        }
    }

    // When a player detected by the collision box leaves the collision zone, move the player out of the list
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            player.Remove(col.gameObject);
        }
    }

    void Update()
    {
        SwitchStates();
    }


    // States switching
    void SwitchStates()
    {
        // Enter attack when a player is present in the player list
        if (player.Count > 0)
        {
            bossstates = BossStates.Attack;
            //Debug.Log("Attack£¡");
        }

        // When no player exists in the player list, go on guard
        else
        {
            bossstates = BossStates.Guard;
            //Debug.Log("Stop Attack£¡");
        }

        // State machine
        switch (bossstates)
        {
            case BossStates.Guard:
                guard();
                break;
            case BossStates.Attack:
                attack();
                break;
        }
    }

    // Mode of attack
    void attack()
    {
        // Always facing the player's location
        Vector3 targetPosition = player[0].transform.position;
        targetPosition.y = boss.position.y;
        boss.LookAt(targetPosition);
        msRender.material.color = Color.Lerp(msRender.material.color, Color.red, smooth * 10);
    }

    // // Mode of guard
    void guard()
    {
        msRender.material.color = Color.Lerp(msRender.material.color, Color.blue, smooth * 10);
    }

}
