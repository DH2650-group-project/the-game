using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject prefab;
    [SerializeField] GameObject player;
    [Range(0, 100)] public int desired_count = 10;

    [SerializeField] float radius = 20;



    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length < desired_count)
        {
            Spawn();
        }
        else if (enemies.Length > desired_count)
        {
            Destroy(enemies[0]);
        }

    }

    void Spawn()
    {

        Vector2 rand_pos = Random.insideUnitCircle * radius + new Vector2(player.transform.position.x, player.transform.position.z);

        Vector3 spawn_pos = new(rand_pos.x, 0, rand_pos.y);

        Quaternion look_at_player = Quaternion.LookRotation(player.transform.position - spawn_pos);

        GameObject enemy = Instantiate(prefab, spawn_pos, look_at_player);
        enemy.GetComponent<FollowPlayer>().player = player;


    }

}


