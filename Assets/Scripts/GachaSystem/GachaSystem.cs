using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GachaSystem : MonoBehaviour
{
    [Header("ĳ���� ���� ���� (1~100)")]
    [Range(1, 100)]
    public int characterLevel = 1;  // �÷��̾��� ���� ���� (Ȯ�� ���� �߽ɰ��� ����)

    [Header("ǥ������: Ŭ���� ���� �л� Ŀ��")]
    public float stdDev = 2.0f;  // ���Ժ����� ǥ������, �������� �� �پ��� ���� ����

    [Header("������ ������ Ŀ���� Ȯ�� ���̺�")]
    public DropTable helmetTable = new DropTable("Helmet"); // ��� ������ ��� ���̺�
    public DropTable armorTable = new DropTable("Armor");   // ���� ������ ��� ���̺�
    public DropTable weaponTable = new DropTable("Weapon"); // ���� ������ ��� ���̺�
    public DropTable cloakTable = new DropTable("Cloak");   // ���� ������ ��� ���̺�

    [Header("õ�� �ý���: ���� ���� �� ����")]
    public int pityThreshold = 10; // ���� ���� �������� �������� ���� �� �ִ� �ִ� Ƚ��
    private int pityCounter = 0;   // ������� ���� ������ ���� Ƚ��

    // ������ ���� Ÿ�� ����
    public enum ItemType { Helmet, Armor, Weapon, Cloak }

    /// <summary>
    /// ������ �󿡼� ĳ���� ������ ������ �� �ڵ����� ��� ���̺� ����
    /// </summary>
    void OnValidate()
    {
        helmetTable.GenerateDistribution(characterLevel, stdDev);
        armorTable.GenerateDistribution(characterLevel, stdDev);
        weaponTable.GenerateDistribution(characterLevel, stdDev);
        cloakTable.GenerateDistribution(characterLevel, stdDev);
    }

    /// <summary>
    /// ���� �̱� ���� �Լ�
    /// ���� ���� ���� �� ���� Ȯ�� ��� �� õ�� �ý��� ���� �� ��� ��ȯ
    /// </summary>
    /// <returns>(����, ������ ����) Ʃ�� ��ȯ</returns>
    public (ItemType itemType, int level) GetRandomItem()
    {
        // 0~3 ���� ���� ������ ���� ����
        ItemType type = (ItemType)UnityEngine.Random.Range(0, 4);

        int level = 1; // �⺻ ������ ����
        DropTable table = GetTable(type); // �ش� ������ ��� ���̺� ��������

        // õ�� �ý���: �������� ���� ������ ������ �ְ� ���� ����
        if (pityCounter >= pityThreshold)
        {
            level = 10;          // �ְ� ���� ������ ����
            pityCounter = 0;     // pity �ʱ�ȭ
        }
        else
        {
            // Ȯ�� ��� ���� �̱�
            level = table.GetWeightedItemLevel();

            // ���� ����(9 �̻�)�̸� pity �ʱ�ȭ, �ƴϸ� ����
            pityCounter = level >= 9 ? 0 : pityCounter + 1;
        }

        // ����� �α� ���
        Debug.Log($"[{type}] Lv.{level} ������ ȹ��");

        return (type, level); // ��� ��ȯ
    }

    /// <summary>
    /// ������ Ÿ�Կ� ���� �ش� ��� ���̺� ��ȯ
    /// </summary>
    /// <param name="type">������ ���� Ÿ��</param>
    /// <returns>�ش� ������ ��� ���̺�</returns>
    DropTable GetTable(ItemType type)
    {
        return type switch
        {
            ItemType.Helmet => helmetTable,
            ItemType.Armor => armorTable,
            ItemType.Weapon => weaponTable,
            ItemType.Cloak => cloakTable,
            _ => helmetTable // �⺻���� ���
        };
    }
}
