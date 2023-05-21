using System.Collections;
using UnityEngine;
using Kryz.CharacterStats;

[CreateAssetMenu(menuName = "Item Effects/Stat Buff")]
public class StatBuffItemEffect : UsableItemEffect
{
	public int StatsABuff;
	public float Duration;

	public override void ExecuteEffect(UsableItem parentItem, Character character)
	{
		StatModifier statModifier = new StatModifier(StatsABuff, StatModType.Flat, parentItem);
		character.StatsA.AddModifier(statModifier);
		character.UpdateStatValues();
		character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
	}

	public override string GetDescription()
	{
		return "Grants " + StatsABuff + " StatsA for " + Duration + " seconds.";
	}

	private static IEnumerator RemoveBuff(Character character, StatModifier statModifier, float duration)
	{
		yield return new WaitForSeconds(duration);
		character.StatsA.RemoveModifier(statModifier);
		character.UpdateStatValues();
	}
}
