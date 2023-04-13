using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerWeapon : MonoBehaviour
{

    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform rightHand;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private Light laserLight;

    private AudioSource audioSource;

    [SerializeField]
    private float laserEnableTime = 0.05f;
    private float laserTimer = 0.0f;
 
    private float fireRate;
    private float fireRateTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        laserLight = firePoint.GetComponent<Light>();
        laserLight.enabled = false;
        audioSource = GetComponent<AudioSource>();
        fireRate = 1/GetComponent<CharacterStats>().inventory1.cdDuration;
    }

    private void Shoot()
    {

        audioSource.Play();

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        lineRenderer.enabled = true;
        laserLight.enabled = true;
        lineRenderer.SetPosition(0, firePoint.position);
        laserTimer = laserEnableTime;

        if(Physics.Raycast(ray, out hit, 20.0f))
        {
            
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.GetPoint(20.0f));    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        laserTimer -= Time.deltaTime;
        fireRateTimer -= Time.deltaTime;

        if(laserTimer <= 0.0f)
        {
            lineRenderer.enabled = false;
            laserLight.enabled = false;
        } 

        if(Input.GetKey(KeyCode.Space) && fireRateTimer <= 0.0f)
        {
            Shoot();
            fireRateTimer = 1.0f / fireRate;
        }
    }
}
