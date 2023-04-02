using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    [SerializeField] private float move_speed = 0.5f;

    [SerializeField] private float angular_speed = 0.5f;

    Rigidbody rb;
    BoxCollider bc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 direction = player.transform.position - transform.position;

        rb.velocity = Vector3.zero;

        Quaternion look_at_player = Quaternion.LookRotation(direction);

        rb.MoveRotation(Quaternion.Slerp(transform.rotation, look_at_player, angular_speed * Time.fixedDeltaTime));

        rb.MovePosition(transform.position + move_speed * Time.fixedDeltaTime * direction.normalized);

    }
}
