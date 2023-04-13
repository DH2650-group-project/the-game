using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;
}
