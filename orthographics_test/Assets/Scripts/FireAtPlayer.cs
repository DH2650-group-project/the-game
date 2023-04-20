using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAtPlayer : MonoBehaviour
{

    [SerializeField] GameObject projectilePrefab;


    [SerializeField] GameObject target;

    [SerializeField] Transform firePoint;

    [SerializeField] Transform gun_pivot; // can only rotate on z axis

    [SerializeField] Transform base_pivot; // can only rotate on y axis

    [SerializeField] float max_vertical_angle = 15f;

    [SerializeField] float projectileSpeed = 10f;

    [SerializeField] float fireRate = 0.5f;

    [SerializeField] int bulletDamage = 1;

    [SerializeField] float firingRange = 10f;

    [SerializeField] float rotationSpeed = 0.5f;

    private Vector3 fire_direction;





    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");

    }



    void FixedUpdate()
    {

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
            return;
        }

        Vector3 direction = new()
        {
            x = target.transform.position.x - transform.position.x,
            y = 0,
            z = target.transform.position.z - transform.position.z
        };
        // rotate direction 90 degrees around y axis
        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;

        // rotate base pivot
        base_pivot.transform.rotation = Quaternion.Slerp(base_pivot.transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        StartCoroutine(Fire());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Fire()
    {

        while (true)
        {
            yield return StartCoroutine(FireOnce());
        }
    }

    IEnumerator FireOnce()
    {
        yield return new WaitForSeconds(1f / fireRate);

        if (target == null)
        {
            yield break;
        }


        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= firingRange)
        {

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            projectile.GetComponent<DamageProjectile>().damage = bulletDamage;
            projectile.GetComponent<DamageProjectile>().targetableLayerMask = LayerMask.GetMask("Player");

            Vector3 direction = -gun_pivot.transform.right;
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

            projectile.transform.forward = direction;

        }
        else
        {
            Debug.Log("Target out of range");
        }

        yield return null;
    }

}
