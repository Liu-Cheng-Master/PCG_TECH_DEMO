using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 offset;
    public Transform player;

    void Start()
    {
        offset = player.position - transform.position;
    }
    void Update()
    {
        transform.position = player.position - offset;
    }
}
