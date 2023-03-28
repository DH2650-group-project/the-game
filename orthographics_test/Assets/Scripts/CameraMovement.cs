using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Vector3 offset = new Vector3(0, 10, -10);

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}
