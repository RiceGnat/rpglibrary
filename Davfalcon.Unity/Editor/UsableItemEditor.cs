using UnityEditor;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(UsableItemDefinition))]
	public class UsableItemEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			UsableItem item = ((UsableItemDefinition)target).obj;
			SetLabelWidth();

			RenderDescriptionField(item);

			Space();

			bool wasSpell = item is SpellItem;
			bool isSpell = Toggle("Spell item", wasSpell);
			item.IsConsumable = Toggle("Consumable", item.IsConsumable);
			item.Remaining = IntField("Remaining uses", item.Remaining);
			item.UsableDuring = (UsableDuringState)EnumMaskPopup("Usable during", item.UsableDuring);

			if (wasSpell != isSpell)
			{
				item = isSpell ? new SpellItem() : new UsableItem();
				((UsableItemDefinition)target).obj = item;
			}

			if (isSpell)
			{
				SpellItem spellItem = item as SpellItem;

				spellItem.Spell = RenderMappedObjectField<ISpell, SpellDefinition, Spell>("Spell", spellItem.Spell, true);

				Space();
			}

			Space();

			RenderEffectsList("Effects", item.Effects, ref ((UsableItemDefinition)target).effectsExpanded);

			CheckChanged(target);
		}
	}
}
