using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

/// <summary>
/// Ŀ���� �ν����� Ŭ����: GachaSystem ��ũ��Ʈ�� ���� ������ �ð�ȭ
/// ������ ��� Ȯ�� ������ �ð������� Ȯ���� �� �ֵ��� ����
/// </summary>
[CustomEditor(typeof(GachaSystem))]
public class GachaSystemEditor : Editor
{
    /// <summary>
    /// �ν����� GUI Ŀ���͸���¡
    /// </summary>
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �׸���
        DrawDefaultInspector();

        // ��� GachaSystem �ν��Ͻ� ����
        GachaSystem gacha = (GachaSystem)target;

        // ���� �ð�ȭ ���� ��
        GUILayout.Label("\n������ ���� ���� (�ð�ȭ)", EditorStyles.boldLabel);

        // �� ������ ��� ���̺� �ð�ȭ
        DrawDistribution("Helmet", gacha.helmetTable);
        DrawDistribution("Armor", gacha.armorTable);
        DrawDistribution("Weapon", gacha.weaponTable);
        DrawDistribution("Cloak", gacha.cloakTable);
    }

    /// <summary>
    /// �� ������ ���� ������ ���� �׷����� �ð�ȭ
    /// </summary>
    /// <param name="label">���� �̸� (��: Helmet)</param>
    /// <param name="table">�ش� ������ ��� ���̺�</param>
    void DrawDistribution(string label, DropTable table)
    {
        // ���� �� ���
        GUILayout.Label($"[{label}]");

        EditorGUILayout.BeginHorizontal(); // ���� �������� ���� ����
        for (int i = 0; i < 10; i++)
        {
            float height = table.weights[i] * 200f; // ���� = Ȯ�� * ������
            Rect rect = GUILayoutUtility.GetRect(10, height); // ���� ���� ����

            // ����: ���� ������ ���� �� ���� ������ �ʷ����� ������ ����
            Color color = Color.Lerp(Color.red, Color.green, i / 9f);
            EditorGUI.DrawRect(rect, color); // ���� �׸���
        }
        EditorGUILayout.EndHorizontal(); // ���� ���̾ƿ� ����
    }
}
#endif
