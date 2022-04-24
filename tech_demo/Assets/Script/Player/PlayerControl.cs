using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    float h;
    float v;
    Rigidbody rb;
    public int speed;
    public Text Count;
    
    int count;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Count.text = "Score: " + count;
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(h, 0, v);
        force *= speed;
        rb.AddForce(force);
    }

    // Destroy the gold and fakewall when player collider with them
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gold")
        {
            Destroy(other.gameObject);
            count++;
        }
        if (other.tag == "FakeWall")
        {
           Destroy(other.gameObject);
        }
    }
}
