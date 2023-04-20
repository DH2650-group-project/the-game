using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    CharacterStats stats;

    [SerializeField] private List<GameObject> deathEffect;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.currentHp <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathEffect[Random.Range(0, deathEffect.Count)], transform.position, Quaternion.identity);
        }
    }
}
