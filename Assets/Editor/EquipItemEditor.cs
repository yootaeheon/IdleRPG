using UnityEditor;
using UnityEngine;
using Inventory.Model;

[CustomEditor(typeof(EquipItemSO))]
public class EquipItemSOEditor : Editor
{
    SerializedProperty equipmentTypeProp;

    SerializedProperty attackPowerProp;
    SerializedProperty attackSpeedProp;
    SerializedProperty criticalChanceProp;

    SerializedProperty plusHpProp;
    SerializedProperty recoverHpProp;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // === ��ӹ��� �⺻ ������Ƽ�� �ڵ� ǥ�� ===
        DrawPropertiesExcluding(serializedObject,
            "_equipmetnType",
            "_attackPower", "_attackSpeed", "_criticalChance",
            "_plusHp", "_recoverHpPerSecond", "_defensePower"
        );

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Equip Section", EditorStyles.boldLabel);

        // EquipmentType ǥ��
        equipmentTypeProp = serializedObject.FindProperty("_equipmetnType");
        EditorGUILayout.PropertyField(equipmentTypeProp);

        EquipmentType type = (EquipmentType)equipmentTypeProp.enumValueIndex;

        // ���� ���� �Ӽ�
        if (type == EquipmentType.Weapon)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Weapon Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_attackPower"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_attackSpeed"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_criticalChance"));
        }

        // �� ���� �Ӽ�
        if (type == EquipmentType.Armor || type == EquipmentType.Helmet || type == EquipmentType.Back)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Armor & Helmet Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_plusHp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_recoverHpPerSecond"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_defensePower"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
