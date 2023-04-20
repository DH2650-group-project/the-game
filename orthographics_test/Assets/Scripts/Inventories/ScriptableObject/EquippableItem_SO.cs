using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EquipmentType
{
	MainBody,
	SubBody,
	DisposableItem,
	Chest,
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem_SO : Item
{
	public int StrengthBonus;
	public int AgilityBonus;
	public int IntelligenceBonus;
	public int VitalityBonus;
	[Space]
	public float StrengthPercentBonus;
	public float AgilityPercentBonus;
	public float IntelligencePercentBonus;
	public float VitalityPercentBonus;
	[Space]
	public EquipmentType EquipmentType;
}
