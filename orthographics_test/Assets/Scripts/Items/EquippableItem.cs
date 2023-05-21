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
    public int StatsABonus;
    public int StatsBBonus;
    public int StatsCBonus;
    public int StatsDBonus;
    [Space]
    public float StatsAPercentBonus;
    public float StatsBPercentBonus;
    public float StatsCPercentBonus;
    public float StatsDPercentBonus;
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
        if (StatsABonus != 0)
            c.StatsA.AddModifier(new StatModifier(StatsABonus, StatModType.Flat, this));
        if (StatsBBonus != 0)
            c.StatsB.AddModifier(new StatModifier(StatsBBonus, StatModType.Flat, this));
        if (StatsCBonus != 0)
            c.StatsC.AddModifier(new StatModifier(StatsCBonus, StatModType.Flat, this));
        if (StatsDBonus != 0)
            c.StatsD.AddModifier(new StatModifier(StatsDBonus, StatModType.Flat, this));

        if (StatsAPercentBonus != 0)
            c.StatsA.AddModifier(new StatModifier(StatsAPercentBonus, StatModType.PercentMult, this));
        if (StatsBPercentBonus != 0)
            c.StatsB.AddModifier(new StatModifier(StatsBPercentBonus, StatModType.PercentMult, this));
        if (StatsCPercentBonus != 0)
            c.StatsC.AddModifier(new StatModifier(StatsCPercentBonus, StatModType.PercentMult, this));
        if (StatsDPercentBonus != 0)
            c.StatsD.AddModifier(new StatModifier(StatsDPercentBonus, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.StatsA.RemoveAllModifiersFromSource(this);
        c.StatsB.RemoveAllModifiersFromSource(this);
        c.StatsC.RemoveAllModifiersFromSource(this);
        c.StatsD.RemoveAllModifiersFromSource(this);
    }

    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }

    public override string GetDescription()
    {
        sb.Length = 0;
        AddStat(StatsABonus, "StatsA");
        AddStat(StatsBBonus, "StatsB");
        AddStat(StatsCBonus, "StatsC");
        AddStat(StatsDBonus, "StatsD");

        AddStat(StatsAPercentBonus, "StatsA", isPercent: true);
        AddStat(StatsBPercentBonus, "StatsB", isPercent: true);
        AddStat(StatsCPercentBonus, "StatsC", isPercent: true);
        AddStat(StatsDPercentBonus, "StatsD", isPercent: true);

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
