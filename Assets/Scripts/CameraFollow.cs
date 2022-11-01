using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerPos;
    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerPos.position.x,playerPos.position.y,playerPos.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.4f,3.4f), Mathf.Clamp(transform.position.y, -2.86f, -2.86f),transform.position.z);
    }
}
