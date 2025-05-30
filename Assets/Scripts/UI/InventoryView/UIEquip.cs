using Inventory.Model;
using Inventory.View;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ���� ��� �����ִ� UI_Progress
/// </summary>
public class UIEquip : MonoBehaviour
{
    [SerializeField] EquipmentManager _equipManager;

    [Header("curItem Sprite")]
    [SerializeField] private Image _helmetImage;
    [SerializeField] private Image _armorSpriteRenderer;
    [SerializeField] private Image _backSpriteRenderer;
    [SerializeField] private Image _weaponSpriteRenderer;

    [Header("��� ���� �� ������ �̹���")]
    [SerializeField] private Sprite emptySlotSprite;

    // ���� �� ��� ����Ʈ
    [SerializeField] List<InventoryItem> _equipItems = new List<InventoryItem>();

    List<UIInventoryItem> _listOfUIEquip = new List<UIInventoryItem>();    // ���� ������ �� ������ UI_Progress ���� ����Ʈ

    private void Awake()
    { 
Init();
    }

    public void OnEnable()
{
    UpdateAllSlots();
}

/// <summary>
/// ��� ������ ������Ʈ
/// </summary>
public void UpdateAllSlots()
{
    UpdateSlot(EquipmentType.Helmet, _helmetImage);
    UpdateSlot(EquipmentType.Armor, _armorSpriteRenderer);
    UpdateSlot(EquipmentType.Back, _backSpriteRenderer);
    UpdateSlot(EquipmentType.Weapon, _weaponSpriteRenderer);
}

/// <summary>
/// �� ������ ���������� ������Ʈ
/// </summary>
public void UpdateSlot(EquipmentType type, Image image)
{
    var item = _equipManager.GetEquippedItem(type);
    if (item != null && item.ItemImage != null)
    {
        image.sprite = item.ItemImage;
            image.color = Color.white;
        }
    else
    {
        image.sprite = emptySlotSprite;
        image.color = new Color(1, 1, 1, 0.4f); // ������ ó��
    }
}

public void Init()
{
    // ���� �� ��񸮽�Ʈ�� ���� ����
    /*_equipItems = new List<InventoryItem>();*/

    // ���� ������ŭ ��� �ִ� ���������� ä��
    for (int i = 0; i < 4; i++)
    {
        _equipItems.Add(InventoryItem.GetEmptyItem());
    }
}
}
