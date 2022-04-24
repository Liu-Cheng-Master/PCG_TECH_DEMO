using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Behave : MonoBehaviour
{
    private MeshRenderer msRender;
    private float smooth = 1.0f;
    public Transform boss;

    // Start is called before the first frame update
    public List<GameObject> player = new List<GameObject>();

    void Start()
    {
        msRender = GetComponent<MeshRenderer>();

        msRender.material.color = Color.blue;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            player.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            player.Remove(col.gameObject);
        }
    }

    public int attackRate = 1;
    public int timer = 0;

    void Update()
    {
        timer += attackRate;
        if(player.Count>0 && timer > attackRate)
        {
            timer -= attackRate;
            attack();
        }
        else
        {
            guard();
        }
        
        if(player.Count>0 && player[0] != null)
        {
            Vector3 targetPosition = player[0].transform.position;
            targetPosition.y = boss.position.y;
            boss.LookAt(targetPosition);
        }
    }

    void attack()
    {
        msRender.material.color = Color.Lerp(msRender.material.color, Color.red, smooth * Time.deltaTime);
    }

    void guard()
    {
        msRender.material.color = Color.Lerp(msRender.material.color, Color.blue, smooth * Time.deltaTime);
    }
}
