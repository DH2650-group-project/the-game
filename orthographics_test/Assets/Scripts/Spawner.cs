using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] GameObject prefab;
    [SerializeField] GameObject player;
    [Range(0, 100)] public int count = 10;

    [SerializeField] float radius = 20;



    private List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new();
    }

    void Update()
    {

        if (enemies.Count < count)
        {
            Spawn();
        }
        else if (enemies.Count > count)
        {
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
        }

    }

    void Spawn()
    {

        Vector2 rand_pos = Random.insideUnitCircle * radius + new Vector2(player.transform.position.x, player.transform.position.z);

        Vector3 spawn_pos = new(rand_pos.x, 0, rand_pos.y);

        Quaternion look_at_player = Quaternion.LookRotation(player.transform.position - spawn_pos);

        GameObject enemy = Instantiate(prefab, spawn_pos, look_at_player);
        enemy.GetComponent<FollowPlayer>().player = player;

        enemies.Add(enemy);

    }

}


