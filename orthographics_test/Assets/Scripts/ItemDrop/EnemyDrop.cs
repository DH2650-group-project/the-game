using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{

    [SerializeField]
    EquippableItem_SO[] loot_table;

    [SerializeField]
    GameObject itemDrop;

    [SerializeField]
    float dropChance = 0.1f;

    private void OnDestroy()
    {
        if(Random.Range(0f, 1f) <= dropChance)
        {
            int randomItem = Random.Range(0, loot_table.Length);
            GameObject thisDrop = Instantiate(itemDrop, transform.position, Quaternion.identity);
            thisDrop.GetComponent<ItemPickUp>().item = loot_table[randomItem];
        }
    }

}
