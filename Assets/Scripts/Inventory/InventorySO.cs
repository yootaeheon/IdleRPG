using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    // �κ��丮�� ���� �����۵��� ����Ʈ
    [SerializeField] List<InventoryItem> _inventoryItems;

    // �κ��丮 ���� ����
    [field: SerializeField] public int Size { get; private set; } = 10;

    /// <summary>
    /// �κ��丮�� �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    public void Init()
    {
        // ������ ����Ʈ�� ���� ����
        _inventoryItems = new List<InventoryItem>();

        // ���� ������ŭ ��� �ִ� ���������� ä��
        for (int i = 0; i < Size; i++)
        {
            _inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    /// <summary>
    /// �������� �κ��丮�� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    public void AddItem(ItemSO item, int quantity)
    {
        // �κ��丮�� �� ������ Ȯ��
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            // ��� �ִ� ������ ã����
            if (_inventoryItems[i].IsEmpty)
            {
                // �ش� ���Կ� �������� �߰�
                _inventoryItems[i] = new InventoryItem()
                {
                    Item = item,
                    Quantity = quantity
                };
                // �������� �� �� �߰��� �� �ݺ��� ���� (�ߺ� �߰� ����)
                break;
            }
        }
    }

    /// <summary>
    /// ���� �κ��丮 ���¸� Dictionary ���·� ��ȯ (�� ������ ����)
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, InventoryItem> GetCurInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            // �� ������ �ǳʶ�
            if (_inventoryItems[i].IsEmpty)
                continue;

            // ���� ��ȣ(index)�� �ش� �������� ��ųʸ��� �߰�
            returnValue[i] = _inventoryItems[i];
        }

        // ���� �������� �ִ� ���Ը� ���Ե� ��ųʸ� ��ȯ
        return returnValue;
    }
}

/// <summary>
/// �κ��丮�� ���� ���� ������ ����ü
/// </summary>
[Serializable]
public struct InventoryItem  // ����ü ���: �� Ÿ���̹Ƿ� �ٸ� ��ũ��Ʈ���� �Ǽ��� �����Ǵ� ���� ����
{
    public int Quantity;      // ������ ����
    public ItemSO Item;       // ������ ������ (ScriptableObject)

    // �������� ��� �ִ��� ���� Ȯ�� (Item�� null�̸� ��� ����)
    public bool IsEmpty => Item == null;

    /// <summary>
    /// ������ ������ ���ο� InventoryItem�� ��ȯ (�� Ÿ���̹Ƿ� ���� ���� �Ұ�)
    /// </summary>
    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem()
        {
            Item = this.Item,
            Quantity = newQuantity,
        };
    }

    /// <summary>
    /// �� �������� �����ϴ� ���� �Լ�
    /// </summary>
    /// <returns></returns>
    public static InventoryItem GetEmptyItem()
        => new InventoryItem()
        {
            Item = null,
            Quantity = 0,
        };
}
