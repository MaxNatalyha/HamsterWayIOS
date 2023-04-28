using System.Collections.Generic;
using SkinSystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Skin), true)]
public class SkinEditor : Editor
{
    private Color _buttonColor = new (0, 1, 0, .33f);
    private Color _availableColor = new (0, 1, 0, .7f);
    private Color _notAvailableColor = new (0, 1, 1, .7f);
        
    private const string AVAILABLE_LABEL = "Доступен";
    private const string NOT_AVAILABLE_LABEL = "Не доступен";
    private const string ASSET_VARIANT_LABEL = "Asset variant";
    private const string COLOR_VARIANT_LABEL = "Color variant";

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space(25);

        Skin skin = (Skin)target;

        DrawButtonsGroup(skin);
        DrawInfoArea(skin);
    }

    private void DrawButtonsGroup(Skin skin)
    {
        GUI.backgroundColor = _buttonColor;
            
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Reset"))
            skin.ResetSkin();

        if (GUILayout.Button("Reset (Default Available)"))
            skin.ResetSkin(true);

        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.white;
    }

    private void DrawInfoArea(ISkin skin)
    {
        EditorGUILayout.LabelField("Skin assets available info", EditorStyles.centeredGreyMiniLabel);

        EditorGUILayout.BeginHorizontal();
            
        EditorGUILayout.LabelField("Base asset");
            
        DrawAvailableInfo(skin.BaseAsset.Bought);

        EditorGUILayout.EndHorizontal();

        if (skin.HasAssetVariants)
            DrawVariantsInfo(skin.AssetsVariants, skin.AssetsVariants.Length, ASSET_VARIANT_LABEL);           
            
        if (skin.HasColorVariants)
            DrawVariantsInfo(skin.ColorVariants, skin.ColorVariants.Length, COLOR_VARIANT_LABEL);
    }

    private void DrawVariantsInfo(IEnumerable<ISkinAsset> variants, int length, string variantLabelName)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();

        for (int i = 0; i < length; i++)
            EditorGUILayout.LabelField(variantLabelName + $" {i}");

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();

        foreach (var variant in variants)
            DrawAvailableInfo(variant.Bought);

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }

    private void DrawAvailableInfo(bool bought)
    {
        GUI.contentColor = bought ? _availableColor : _notAvailableColor;
        EditorGUILayout.LabelField(bought ? AVAILABLE_LABEL : NOT_AVAILABLE_LABEL, EditorStyles.boldLabel);
        GUI.contentColor = Color.white;
    }
}