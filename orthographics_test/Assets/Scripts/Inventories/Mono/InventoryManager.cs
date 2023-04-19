using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;

    private void Awake()
    {
        inventory.OnItemRightClickedEvent += EquipItemFromInventory;
        equipmentPanel.OnItemRightClickedEvent += UnequipFromEquipPanel;
    }
    private void EquipItemFromInventory(Item item)
    {
        if(item is EquippableItem_SO)
        {
            Equip((EquippableItem_SO)item);
        }
    }
    private void UnequipFromEquipPanel(Item item)
    {
        if (item is EquippableItem_SO)
        {
            Unequip((EquippableItem_SO)item);
        }
    }

    public void Equip(EquippableItem_SO item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem_SO previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem_SO item)
    {
        if(!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
        }
    }
}
