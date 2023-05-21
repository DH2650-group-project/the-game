using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Serializable]
    public struct DropPair
    {
        public EquippableItem_SO item;
        public float dropChance;
        public DropPair(EquippableItem_SO item, float dropChance)
        {
            this.item = item;
            this.dropChance = dropChance;
        }
    }

    [SerializeField]
    DropPair[] loot_table;

    [SerializeField]
    GameObject itemDrop;

    private void OnDestroy()
    {

        float dropRoll = UnityEngine.Random.Range(0f, 1f);
        int lowest = -1;

        for (int i = 0; i < loot_table.Length; i++) 
        {
            if(dropRoll <= loot_table[i].dropChance)
            {
                if(lowest == -1)
                {
                    lowest = i;
                }
                else if (loot_table[i].dropChance < loot_table[lowest].dropChance)
                {
                    lowest = i;
                }
            }
        }

        if(lowest != -1)
        {
            GameObject drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
            drop.GetComponent<ItemPickUp>().item = loot_table[lowest].item;
        }
        else
        {
            Debug.Log("No item dropped");
        }
    }

}
