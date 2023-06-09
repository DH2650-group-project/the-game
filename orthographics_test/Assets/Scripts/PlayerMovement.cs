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

    [SerializeField]
    private float dashDuration = 0.5f;
    [SerializeField]
    private float dashSpeed = 20.0f;
    private float dashTimer = 0.0f;

    [SerializeField]
    ParticleSystem dashEffect;

    private float currentSpeed;

    private Character playerCharacter;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerCharacter = GetComponent<Character>();

        currentSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            anim.SetTrigger("Shoot");

        speed = 6.0f + (float)playerCharacter.Speed.Value / 10.0f;
        dashSpeed = 12.0f + (float)playerCharacter.Speed.Value / 10.0f;

        if (dashTimer <= 0.0f)
            currentSpeed = speed;

        // if shift is pressed, increase speed
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0.0f)
        {
            currentSpeed = dashSpeed;
            dashTimer = dashDuration;
            dashEffect.transform.LookAt(transform.position - rb.velocity);
            dashEffect.Play();
        }
        dashTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.velocity =
            new Vector3(Input.GetAxisRaw("Vertical") + Input.GetAxisRaw("Horizontal"),
            0,
            Input.GetAxisRaw("Vertical") - Input.GetAxisRaw("Horizontal"));


        if (rb.velocity.sqrMagnitude > 1)
            rb.velocity = rb.velocity.normalized;

        rb.velocity *= currentSpeed;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);
        groundPlane.Raycast(ray, out float rayDistance);
        Vector3 point = ray.GetPoint(rayDistance);

        if((point - transform.position).sqrMagnitude > 0.2)
            transform.LookAt(point);


        anim.SetFloat("speed", rb.velocity.sqrMagnitude);

        // change speed of animation based on speed of player
        //This is a bit bugged should change it later
        //anim.speed = rb.velocity.magnitude / speed;

    }
}
