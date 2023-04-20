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


    [SerializeField] private bool friendlyFire = false;


    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindWithTag("Player");

        InvokeRepeating(nameof(Fire), 0f, 1f / fireRate);



    }

    // when change to firerate occurs, cancel the previous invoke and start a new one
    void OnValidate()
    {
        CancelInvoke(nameof(Fire));
        InvokeRepeating(nameof(Fire), 0f, 1f / fireRate);
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

    void Fire()
    {

        if (target == null)
        {
            return;
        }


        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= firingRange)
        {

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            DamageProjectile damageProjectile = projectile.GetComponent<DamageProjectile>();
            damageProjectile.damage = bulletDamage;
            damageProjectile.targetableLayerMask = LayerMask.GetMask("Player");
            if (friendlyFire)
            {
                damageProjectile.targetableLayerMask = LayerMask.GetMask("Enemy");
            }
            damageProjectile.owner = gameObject;

            Vector3 direction = -gun_pivot.transform.right;
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

            projectile.transform.forward = direction;

        }
        else
        {
            Debug.Log("Target out of range");
        }
    }

}
