using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")] //more stats can be add later, just making a template for character stats
    public int maxHp;
    public int currentHp;
    public int maxMp;
    public int currentMp;
    public int speed;
    public Inventory_SO inventory1;
/***
    public int Inventory2;
    public int Inventory3;
    public int Inventory4;
    public int Inventory5;
***/

    //I plan to add another scriptable object to store the attack and defense individually, so no atk or def in this object
}
