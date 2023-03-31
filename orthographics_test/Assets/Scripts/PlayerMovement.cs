using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    [SerializeField]
    private float speed = 10;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            anim.SetTrigger("Shoot");
    }

    private void FixedUpdate()
    {
        rb.velocity =
            new Vector3(Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical") - Input.GetAxisRaw("Horizontal"));

        if (rb.velocity.sqrMagnitude > 1)
            rb.velocity = rb.velocity.normalized;

        rb.velocity *= speed;

        transform.LookAt(transform.position + rb.velocity);

        anim.SetFloat("speed", rb.velocity.sqrMagnitude);
    }
}
