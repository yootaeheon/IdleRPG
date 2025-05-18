using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������ ����/����/��ȸ �����ϴ� Ŭ����
/// �� EquipmentType�� ���� �ϳ��� �����۸� ���� ����
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    /// <summary>
    /// ���� �������� �����۵� �����ϴ� ��ųʸ�
    /// Ű : ��� Ÿ�� (Ex> Aromor, Weapon ��)
    /// �� : �ش� Ÿ�Կ� ������ EquipItemSO
    /// </summary>
    private Dictionary<EquipmentType, EquipItemSO> _equippedItems = new();

    /// <summary>
    /// ��� ���� �޼���
    /// </summary>
    /// <param name="item"></param>
    public void EquipItem(EquipItemSO item)
    {
        _equippedItems[item.EquipmentType] = item;
        // ���� �ݿ� ���� ��...
    }

    /// <summary>
    /// ��� ���� ���� �޼���
    /// �������� ��� ��ųʸ����� ����
    /// </summary>
    /// <param name="type"></param>
    public void UnequipItem(EquipmentType type)
    {
        if (_equippedItems.ContainsKey(type))
            _equippedItems.Remove(type);
    }

    /// <summary>
    /// Ư�� EquipmentType�� ������ �������� ��ȯ
    /// ������ null ��ȯ.
    /// </summary>
    /// <param name="type">��ȸ�� ����� Ÿ��</param>
    /// <returns>������ ��� ������ �Ǵ� null</returns>
    public EquipItemSO GetEquippedItem(EquipmentType type)
    {
        _equippedItems.TryGetValue(type, out EquipItemSO item);
        return item;
    }
}
