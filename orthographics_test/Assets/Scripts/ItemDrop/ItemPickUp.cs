using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public EquippableItem item;

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character != null)
        {
            Inventory inventory = character.Inventory;
            if (inventory != null)
            {
                if (inventory.AddItem(item))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
