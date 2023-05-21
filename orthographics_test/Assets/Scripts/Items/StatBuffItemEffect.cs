using System.Collections;
using UnityEngine;
using Kryz.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
    public int HPBuff;
    public float Duration;

    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatModifier statModifier = new StatModifier(HPBuff, StatModType.Flat, parentItem);
        character.HP.AddModifier(statModifier);
        character.UpdateStatValues();
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
    }

    public override string GetDescription()
    {
        return "Grants " + HPBuff + " HP for " + Duration + " seconds.";
    }

    private static IEnumerator RemoveBuff(Character character, StatModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.HP.RemoveModifier(statModifier);
        character.UpdateStatValues();
    }
}
