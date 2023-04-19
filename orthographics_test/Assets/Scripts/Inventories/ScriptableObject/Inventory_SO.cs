using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInventory", menuName = "Inventory/data")]

public class Inventory_SO : ScriptableObject
{
    [Header("Item Info")]
    public int id;
    public GameObject uiImage;
    public GameObject Prefab;
    public float atk; //if it is an armor or shield, this one represents the amount of shield
    public float def; //Even for weapons there are defense data, since we assume every part can be destroyed
    public float weight; // for calculating the speed of the player
    public float cdDuration; //shooting speed or whatever it should be applied
    public int type; //0 for kinetic, 1 for energy, 2 for exploration
}
