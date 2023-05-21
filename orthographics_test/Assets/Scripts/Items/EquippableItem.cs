using UnityEngine;
using Kryz.CharacterStats;

public enum EquipmentType
{
    Body,
    SubBody,
    Chest,
    Wheels,
}

[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : Item
{
    public int HPBonus;
    public int EnergyBonus;
    public int DamageBonus;
    public int SpeedBonus;
    [Space]
    public float HPPercentBonus;
    public float EnergyPercentBonus;
    public float DamagePercentBonus;
    public float SpeedPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    // public override void Destroy()
    // {
    //     Destroy(this);
    // }

    public void Equip(Character c)
    {
        if (HPBonus != 0)
            c.HP.AddModifier(new StatModifier(HPBonus, StatModType.Flat, this));
        if (EnergyBonus != 0)
            c.Energy.AddModifier(new StatModifier(EnergyBonus, StatModType.Flat, this));
        if (DamageBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamageBonus, StatModType.Flat, this));
        if (SpeedBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedBonus, StatModType.Flat, this));

        if (HPPercentBonus != 0)
            c.HP.AddModifier(new StatModifier(HPPercentBonus, StatModType.PercentMult, this));
        if (EnergyPercentBonus != 0)
            c.Energy.AddModifier(new StatModifier(EnergyPercentBonus, StatModType.PercentMult, this));
        if (DamagePercentBonus != 0)
            c.Damage.AddModifier(new StatModifier(DamagePercentBonus, StatModType.PercentMult, this));
        if (SpeedPercentBonus != 0)
            c.Speed.AddModifier(new StatModifier(SpeedPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.HP.RemoveAllModifiersFromSource(this);
        c.Energy.RemoveAllModifiersFromSource(this);
        c.Damage.RemoveAllModifiersFromSource(this);
        c.Speed.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(HPBonus, "HP");
        AddStat(EnergyBonus, "Energy");
        AddStat(DamageBonus, "Damage");
        AddStat(SpeedBonus, "Speed");

        AddStat(HPPercentBonus, "HP", isPercent: true);
        AddStat(EnergyPercentBonus, "Energy", isPercent: true);
        AddStat(DamagePercentBonus, "Damage", isPercent: true);
        AddStat(SpeedPercentBonus, "Speed", isPercent: true);

        return sb.ToString();
    }

    private void AddStat(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append("+");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }
            sb.Append(statName);
        }
    }
}
