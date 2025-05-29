using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������ ����/����/��ȸ �����ϴ� Ŭ����
/// �� EquipmentType�� ���� �ϳ��� �����۸� ���� ����
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    [SerializeField] CharacterModel _model;

    [SerializeField] SpriteRenderer _helmetSpriteRenderer;
    [SerializeField] SpriteRenderer _armorSpriteRenderer;
    [SerializeField] SpriteRenderer _backSpriteRenderer;
    [SerializeField] SpriteRenderer _weaponSpriteRenderer;

    private Dictionary<EquipmentType, EquipItemSO> _equippedItems = new();

    public EquipItemSO SelectedItem { get; set;}

    /// <summary>
    /// ��� ���� �޼���
    /// </summary>
    /// <param name="item">������ ������</param>
    public void EquipItem(EquipItemSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("������ �������� null�Դϴ�.");
            return;
        }

        // ������ Ÿ���� EquipmentType�� �ش��ϴ��� Ȯ��
        if (!(item is EquipItemSO))
        {
            Debug.LogWarning("�� �������� ��� �������� �ƴմϴ�. ������ �� �����ϴ�.");
            return;
        }

        if (_equippedItems.TryGetValue(item.EquipmentType, out var equipped))
        {
            UnEquipItem(item.EquipmentType);
        }

        _equippedItems[item.EquipmentType] = item;

        switch (item.EquipmentType)
        {
            case EquipmentType.Helmet:
                _helmetSpriteRenderer.sprite = item.ItemImage;
                ApplyStat(item);
                break;
            case EquipmentType.Armor:
                _armorSpriteRenderer.sprite = item.ItemImage;
                ApplyStat(item);
                break;
            case EquipmentType.Back:
                _backSpriteRenderer.sprite = item.ItemImage;
                ApplyStat(item);
                break;
            case EquipmentType.Weapon:
                _weaponSpriteRenderer.sprite = item.ItemImage;
                _model.AttackPower += item.AttackPower;
                _model.AttackSpeed += item.AttackSpeed;
                _model.CriticalChacnce += item.CriticalChance;
                break;
        }
    }

    /// <summary>
    /// ��� ���� ���� �޼��� (EquipItemSO ���)
    /// </summary>
    /// <param name="item">������ ������</param>
    public void UnEquipSelectedItem(EquipItemSO item)
    {
        if (item == null)
        {
            Debug.LogWarning("������ �������� null�Դϴ�.");
            return;
        }

        UnEquipItem(item.EquipmentType);
    }

    /// <summary>
    /// Ÿ�� ��� ��� ����
    /// </summary>
    public void UnEquipItem(EquipmentType type)
    {
        if (_equippedItems.TryGetValue(type, out var item))
        {
            // ���� ����
            if (item.EquipmentType == EquipmentType.Weapon)
            {
                _model.AttackPower -= item.AttackPower;
                _model.AttackSpeed -= item.AttackSpeed;
                _model.CriticalChacnce -= item.CriticalChance;
            }
            else
            {
                _model.MaxHp -= item.PlusMaxHp;
                _model.CurHp = Mathf.Min(_model.CurHp, _model.MaxHp);
                _model.DefensePower -= item.DefensePower;
                _model.RerecoverHpPerSecond -= item.RecoverHpPerSecond;
            }

            // ��������Ʈ �ʱ�ȭ
            switch (type)
            {
                case EquipmentType.Helmet:
                    _helmetSpriteRenderer.sprite = null;
                    break;
                case EquipmentType.Armor:
                    _armorSpriteRenderer.sprite = null;
                    break;
                case EquipmentType.Back:
                    _backSpriteRenderer.sprite = null;
                    break;
                case EquipmentType.Weapon:
                    _weaponSpriteRenderer.sprite = null;
                    break;
            }

            _equippedItems.Remove(type);
        }
    }

    /// <summary>
    /// ��� ���� ���� (�� ����)
    /// </summary>
    private void ApplyStat(EquipItemSO item)
    {
        _model.MaxHp += item.PlusMaxHp;
        _model.CurHp += item.PlusMaxHp;
        _model.DefensePower += item.DefensePower;
        _model.RerecoverHpPerSecond += item.RecoverHpPerSecond;
    }

    /// <summary>
    /// Ư�� Ÿ�Կ� ������ ������ ��ȯ
    /// </summary>
    public EquipItemSO GetEquippedItem(EquipmentType type)
    {
        _equippedItems.TryGetValue(type, out EquipItemSO item);
        return item;
    }

    public void Button_Equip()
    {
        if (SelectedItem == null)
        {
            Debug.LogWarning("[EquipManager] ������ ��� �������� ���õ��� �ʾҽ��ϴ�.");
            return;
        }


        EquipItem(SelectedItem);
    }

    public ItemSO SetSelectedItem(ItemSO item)
    {
        // ������ Ÿ�� üũ �� �Ҵ�
        if (item is EquipItemSO equipItem)
        {
            SelectedItem = equipItem;
            Debug.Log($"{SelectedItem}�� ���õ�");
            return SelectedItem;
        }
        else
        {
            Debug.LogWarning("���õ� �������� ��� �������� �ƴմϴ�.");
            SelectedItem = null;
            return null;
        }
    }
}
