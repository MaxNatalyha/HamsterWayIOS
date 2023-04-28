using SkinSystem;
using UnityEngine;

[UnityEditor.CustomPropertyDrawer(typeof(SkinAssetID))]
public class AssetIDDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        UnityEditor.EditorGUI.BeginProperty(position, label, property);

        position = UnityEditor.EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        GUI.enabled = false;
        Rect valueRect = position;
        UnityEditor.SerializedProperty idProperty = property.FindPropertyRelative("value");
        UnityEditor.EditorGUI.PropertyField(valueRect, idProperty, GUIContent.none);

        GUI.enabled = true;

        UnityEditor.EditorGUI.EndProperty();
    }
}