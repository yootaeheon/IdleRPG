using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;

//Back, BodyArmor, 12_Helmet2, R_Weapon�� �ٲ�� ���� �ٲ�


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

    /// <summary>
    /// ���� �������� �����۵� �����ϴ� ��ųʸ�
    /// Ű : ��� Ÿ�� (Ex> Aromor, Weapon ��)
    /// �� : �ش� Ÿ�Կ� ������ EquipItemSO
    /// ������ ����� ���ȸ�ŭ �÷���
    /// </summary>
    private Dictionary<EquipmentType, EquipItemSO> _equippedItems = new();

    /// <summary>
    /// ��� ���� �޼���
    /// </summary>
    /// <param name="item"></param>
    public void EquipItem(EquipItemSO item)
    {
        if (_equippedItems.TryGetValue(item.EquipmentType, out var equipped))
        {
            UnEquipItem(item.EquipmentType);
        }

        _equippedItems[item.EquipmentType] = item;

        switch (item.EquipmentType)
        {
            case EquipmentType.Helmet:
                _helmetSpriteRenderer.sprite = item.ItemImage;
                _model.MaxHp += item.PlusMaxHp;
                _model.CurHp += item.PlusMaxHp; // HP�� �״�� �����ϸ鼭 Max �����и�ŭ ȸ��
                _model.DefensePower += item.DefensePower;
                _model.RerecoverHpPerSecond += item.RecoverHpPerSecond;
                break;
            case EquipmentType.Armor:
                _armorSpriteRenderer.sprite = item.ItemImage;
                _model.MaxHp += item.PlusMaxHp;
                _model.CurHp += item.PlusMaxHp; // HP�� �״�� �����ϸ鼭 Max �����и�ŭ ȸ��
                _model.DefensePower += item.DefensePower;
                _model.RerecoverHpPerSecond += item.RecoverHpPerSecond;
                break;
            case EquipmentType.Back:
                _backSpriteRenderer.sprite = item.ItemImage;
                _model.MaxHp += item.PlusMaxHp;
                _model.CurHp += item.PlusMaxHp; // HP�� �״�� �����ϸ鼭 Max �����и�ŭ ȸ��
                _model.DefensePower += item.DefensePower;
                _model.RerecoverHpPerSecond += item.RecoverHpPerSecond;
                break;
            case EquipmentType.Weapon:
                _weaponSpriteRenderer.sprite = item.ItemImage;
                _model.AttackPower += item.AttackPower;
                _model.AttackSpeed += item.AttackSpeed;
                _model.CriticalChacnce += item.CriticalChance;
                break;
        };
    }

    /// <summary>
    /// ��� ���� ���� �޼���
    /// �������� ����� ���� ���̳ʽ�
    /// �������� ��� ��ųʸ����� ����
    /// </summary>
    /// <param name="type"></param>
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
                _model.CurHp = Mathf.Min(_model.CurHp, _model.MaxHp); // MaxHp���� ū ��� ����

                _model.DefensePower -= item.DefensePower;
                _model.RerecoverHpPerSecond -= item.RecoverHpPerSecond;
            }
            _equippedItems.Remove(type);
        }
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
