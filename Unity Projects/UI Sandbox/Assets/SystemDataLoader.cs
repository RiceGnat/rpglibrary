using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using RPGLibrary;
using RPGLibrary.Randomization;
using UnityEditor;
using UnityEngine;

namespace Davfalcon.Unity
{
	public static class SystemDataLoader
	{
		private static bool loadedEffects = false;

		[RuntimeInitializeOnLoadMethod]
		public static void LoadData()
		{
			LoadEffects();
			LoadBuffs();
			LoadEquipment();
			LoadSpells();
		}

		[InitializeOnLoadMethod]
		public static void LoadEffects()
		{
			if (!loadedEffects)
			{
				IEffectFactory effects = SystemData.Current.Effects;

				effects.LoadEffect("Burn", (definition, unit, source, originator, value) =>
				{
					int hpPercent = (int)definition.Args[0];
					int cap = (int)(definition.Args[1] ?? 0);

					Damage d = new Damage(
							DamageType.Magical,
							Element.Fire,
							unit.Stats[CombatStats.HP].Cap(cap, hpPercent),
							source.SourceName);

					return new LogEntry(string.Format("{0} is burned for {1} HP.", unit.Name, unit.ReceiveDamage(d).Value));
				});

				effects.LoadEffect("Poison", (definition, unit, source, originator, value) =>
				{
					int hpPercent = (int)definition.Args[0];

					Damage d = new Damage(
							DamageType.True,
							Element.Neutral,
							unit.Stats[CombatStats.HP].Cap(0, hpPercent),
							source.SourceName);

					return new LogEntry(string.Format("{0} loses {1} HP to poison.", unit.Name, unit.ReceiveDamage(d).Value));
				});

				effects.LoadEffect("ElementDmg", (definition, unit, source, originator, value) =>
				{
					Element dmgElement = (Element)definition.Args[0];
					int dmgValue = (int)definition.Args[1];

					Damage d = new Damage(
							DamageType.Magical,
							dmgElement,
							dmgValue,
							source.SourceName);

					HPLoss h = unit.ReceiveDamage(d);

					return new LogEntry(d.LogWith(h));
				});

				effects.LoadEffect("Heal", (definition, unit, source, originator, value) =>
				{
					int hpPercent = (int)definition.Args[0];
					int cap = (int)(definition.Args[1] ?? 0);

					int heal = unit.Stats[CombatStats.HP].Cap(cap, hpPercent);
					return new LogEntry(string.Format("{0} recovers {1} HP.", unit.Name, unit.ChangeHP(heal)));
				});

				effects.LoadEffect("Lifelink", (definition, unit, source, originator, value) =>
				{
					int gainPercent = (int)definition.Args[0];

					int hp = originator.ChangeHP(value * gainPercent / 100);
					return new LogEntry(string.Format("{0} gains {1} HP.", originator.Name, hp));
				});

				effects.LoadEffect("DebuffChance", (definition, unit, source, originator, value) =>
				{
					string debuffName = (string)definition.Args[0];
					int successChance = (int)definition.Args[1];

					IBuff debuff = SystemData.Current.Buffs.Get(debuffName);
					ISuccessCheck rand = new SuccessChecker(successChance / 100f);

					if (rand.Check())
					{
						unit.ApplyBuff(debuff, string.Format("{0}'s {1}", originator.Name, source.SourceName));
						return new LogEntry(string.Format("{0} is affected by {1}.", unit.Name, debuff.Name));
					}
					else return null;
				});

				loadedEffects = true;
			}
		}

		public static void LoadBuffs()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:BuffDefinition"))
			{
				SystemData.Current.Buffs.Load(AssetDatabase.LoadAssetAtPath<BuffDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}

		public static void LoadEquipment()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:EquipmentDefinition"))
			{
				SystemData.Current.Equipment.Load(AssetDatabase.LoadAssetAtPath<EquipmentDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}

		public static void LoadSpells()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:SpellDefinition"))
			{
				SystemData.Current.Spells.Load(AssetDatabase.LoadAssetAtPath<SpellDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}
	}
}
