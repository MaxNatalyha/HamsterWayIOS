using Services;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RewardConfig))]
public class RewardConfigEditor : Editor
{
    private int _easyValue;
    private int _classicValue;
    private int _hardValue;
    private int _matchingCardsValue;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RewardConfig config = (RewardConfig)target;

        EditorGUILayout.Space(25);
        EditorGUILayout.HelpBox("Reward info calculator", MessageType.None);

        DrawInfoSlider(config.easyRewardCurve, "Pipeline easy levels", ref _easyValue);
        DrawInfoSlider(config.classicRewardCurve, "Pipeline classic levels", ref _classicValue);
        DrawInfoSlider(config.hardRewardCurve, "Pipeline hard levels", ref _hardValue);
        DrawInfoSlider(config.matchingCardRewardCurve, "Matching Cards levels", ref _matchingCardsValue);
    }

    private void DrawInfoSlider(AnimationCurve curve, string label, ref int value)
    {
        int maxValue = Mathf.RoundToInt(curve.keys[curve.length - 1].time);
        EditorGUILayout.LabelField(label, EditorStyles.centeredGreyMiniLabel);
        
        value = EditorGUILayout.IntSlider(value, 0, maxValue);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField($"Награда за {value} уровень ");
        EditorGUILayout.LabelField($"{Mathf.RoundToInt(curve.Evaluate(value))} coins", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField($"Общая награда за {value} уровней ");

        float total = 0;
        for (int i = 0; i < value; i++)
            total += curve.Evaluate(i);
        

        EditorGUILayout.LabelField($"{Mathf.RoundToInt(total)} coins", EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(25);
    }
}