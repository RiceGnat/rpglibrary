using UnityEditor;
using UnityEngine;

namespace Davfalcon.Revelator.Unity.UI.Editor
{
	[CustomPropertyDrawer(typeof(StatsView.AttributeBinding))]
	public class AttributeBindingDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			int enumWidth = 50;
			Rect enumRect = new Rect(position.x, position.y, enumWidth, position.height);
			Rect fieldRect = new Rect(position.x + enumWidth + 5, position.y, position.width - enumWidth - 5, position.height);

			EditorGUI.PropertyField(enumRect, property.FindPropertyRelative("attribute"), GUIContent.none);
			EditorGUI.PropertyField(fieldRect, property.FindPropertyRelative("textElement"), GUIContent.none);

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}
