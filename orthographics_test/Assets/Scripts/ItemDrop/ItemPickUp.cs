using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public EquippableItem_SO item;

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if(inventory != null)
        {
            if (inventory.AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}
