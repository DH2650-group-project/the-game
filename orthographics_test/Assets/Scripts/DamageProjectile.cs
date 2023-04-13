using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageProjectile : MonoBehaviour
{

    public int damage = 1;

    // When the projectile is spawned by an enemy, it should only hit the player
    // When the projectile is spawned by the player, it should only hit enemies
    public LayerMask targetableLayerMask;

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // TODO: if other is a targetable object, deal damage to it
        if (targetableLayerMask == (targetableLayerMask | (1 << other.gameObject.layer)))
        {
            other.GetComponent<CharacterStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
