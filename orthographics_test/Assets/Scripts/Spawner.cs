using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject[] prefab;
    [SerializeField] GameObject player;
    [Range(0, 100)] public int desired_count = 10;

    [Range(0, 1)] public float spawn_height = 0;

    [SerializeField] BoxCollider[] spawn_zones;

    private GameObject[] enemies;


    [SerializeField] int time_before_spawn = 10;

    private float time_at_start;


    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        time_at_start = Time.time;

    }

    void Update()
    {
        if (Time.time < time_at_start + time_before_spawn)
        {
            return;
        }

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
        GameObject prefab = this.prefab[Random.Range(0, this.prefab.Length)];

        Vector2 rand_pos = RandomPointInside(spawn_zones[Random.Range(0, spawn_zones.Length)]);

        Vector3 spawn_pos = new(rand_pos.x, spawn_height, rand_pos.y);

        Quaternion look_at_player = Quaternion.LookRotation(player.transform.position - spawn_pos);

        GameObject enemy = Instantiate(prefab, spawn_pos, look_at_player);
        enemy.GetComponent<FollowPlayer>().player = player;



    }

    Vector2 RandomPointInside(BoxCollider zone)
    {
        Vector2 random_point = new(
            Random.Range(zone.bounds.min.x, zone.bounds.max.x),
            Random.Range(zone.bounds.min.z, zone.bounds.max.z)
        );

        return random_point;
    }

}


