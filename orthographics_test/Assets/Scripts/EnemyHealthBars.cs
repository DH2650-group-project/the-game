using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyHealthBars : MonoBehaviour
{

    public GameObject healthBarPrefab;

    private HashSet<GameObject> trackedEnemies;
    void Start()
    {
        trackedEnemies = new();
        healthBarPrefab.SetActive(false);
    }

    void Update()
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (!trackedEnemies.Contains(enemies[i]))
            {
                trackedEnemies.Add(enemies[i]);
                GameObject healthBar = Instantiate(healthBarPrefab, transform);
                healthBar.GetComponent<UIFollowObject>().target = enemies[i];
                healthBar.GetComponent<HealthBar>().stats = enemies[i].GetComponent<CharacterStats>();
                healthBar.SetActive(true);
            }
        }

        foreach (GameObject enemy in trackedEnemies.ToList())
        {
            if (enemy == null)
            {
                trackedEnemies.Remove(enemy);
            }
        }

    }

}

