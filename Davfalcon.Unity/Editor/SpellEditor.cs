using UnityEditor;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(SpellDefinition))]
	public class SpellEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Spell spell = ((SpellDefinition)target).obj;
			SetLabelWidth();

			RenderDescriptionField(spell);

			Space();

			spell.TargetType = (SpellTargetType)EnumPopup("Target type", spell.TargetType);
			spell.SpellElement = (Element)EnumPopup("Element", spell.SpellElement);
			spell.DamageType = (DamageType)EnumPopup("Damage type", spell.DamageType);
			spell.Cost = IntField("MP cost", spell.Cost);
			spell.BaseDamage = IntField("Base damage", spell.BaseDamage);
			spell.BaseHeal = IntField("Base heal", spell.BaseHeal);
			spell.Range = IntField("Range", spell.Range);
			spell.Size = IntField("Size", spell.Size);
			spell.MaxTargets = IntField("Max targets", spell.MaxTargets);

			Space();

			RenderBuffsList(spell.GrantedBuffs, "Applied buffs", true, ref ((SpellDefinition)target).buffsExpanded);

			Space();

			RenderEffectsList(spell.CastEffects, "Cast effects", ref ((SpellDefinition)target).effectsExpanded);

			CheckChanged(target);
		}
	}
}
