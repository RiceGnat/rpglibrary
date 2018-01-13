using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using RPGLibrary;
using RPGLibrary.Randomization;
using UnityEditor;
using UnityEngine;
using static Davfalcon.Engine.SystemData;

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
				IEffectFactory effects = Current.Effects;

				effects.LoadEffect("Burn", (definition, unit, source, originator, value) =>
				{
					Damage d = new Damage(
							DamageType.Magical,
							Element.Fire,
							unit.Stats[CombatStats.HP].Cap((int)(definition.Args?[1] ?? 0), (int)definition.Args[0]),
							source.SourceName);

					return new LogEntry(string.Format("{0} is burned for {1} HP.", unit.Name, unit.ReceiveDamage(d).Value));
				});

				effects.LoadEffect("Poison", (definition, unit, source, originator, value) =>
				{
					Damage d = new Damage(
							DamageType.True,
							Element.Neutral,
							unit.Stats[CombatStats.HP].Cap(0, (int)definition.Args[0]),
							source.SourceName);

					return new LogEntry(string.Format("{0} loses {1} HP to poison.", unit.Name, unit.ReceiveDamage(d).Value));
				});

				effects.LoadEffect("ElementDmg", (definition, unit, source, originator, value) =>
				{
					Damage d = new Damage(
							DamageType.Magical,
							(Element)definition.Args[0],
							(int)definition.Args[0],
							source.SourceName);

					HPLoss h = unit.ReceiveDamage(d);

					return new LogEntry(d.LogWith(h));
				});

				effects.LoadEffect("Heal", (definition, unit, source, originator, value) =>
				{
					int heal = unit.Stats[CombatStats.HP].Cap((int)(definition.Args?[1] ?? 0), (int)definition.Args[0]);
					return new LogEntry(string.Format("{0} recovers {1} HP.", unit.Name, unit.ChangeHP(heal)));
				});

				effects.LoadEffect("Lifelink", (definition, unit, source, originator, value) =>
				{
					int hp = originator.ChangeHP(value * (int)definition.Args[0] / 100);
					return new LogEntry(string.Format("{0} gains {1} HP.", originator.Name, hp));
				});

				effects.LoadEffect("DebuffChance", (definition, unit, source, originator, value) =>
				{
					IBuff debuff = Current.Buffs.Get((string)definition.Args[0]);
					ISuccessCheck rand = new SuccessChecker((int)definition.Args[1] / 100f);

					if (rand.Check())
					{
						unit.ApplyBuff(debuff, string.Format("{0}'s {1}", originator.Name, source.SourceName));
						return new LogEntry(string.Format("{0} is affected by {1}.", unit.Name, debuff.Name));
					}
					else return null;
				});
			}
			loadedEffects = true;
		}

		public static void LoadBuffs()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:BuffDefinition"))
			{
				Current.Buffs.Load(AssetDatabase.LoadAssetAtPath<BuffDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}

		public static void LoadEquipment()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:EquipmentDefinition"))
			{
				Current.Equipment.Load(AssetDatabase.LoadAssetAtPath<EquipmentDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}

		public static void LoadSpells()
		{
			foreach (string guid in AssetDatabase.FindAssets("t:SpellDefinition"))
			{
				Current.Spells.Load(AssetDatabase.LoadAssetAtPath<SpellDefinition>(AssetDatabase.GUIDToAssetPath(guid)).obj);
			}
		}
	}
}
