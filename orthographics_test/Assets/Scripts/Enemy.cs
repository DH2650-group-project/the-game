using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    CharacterStats stats;


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
        }
    }
}
