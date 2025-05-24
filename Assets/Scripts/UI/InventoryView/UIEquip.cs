using Inventory.Model;
using Inventory.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���� ���� ��� �����ִ� UI_Progress
/// </summary>
public class UIEquip : MonoBehaviour
{
    [SerializeField] EquipmentManager _equipManager;

    [SerializeField] InventorySO _inventorySO;

    [SerializeField] Transform _parentTransform;

    [SerializeField] UIInventoryItem _prefab;
 
    [Header("UI_Progress Slot Images")]
    [SerializeField] private Image helmetSlot;
    [SerializeField] private Image armorSlot;
    [SerializeField] private Image backSlot;
    [SerializeField] private Image weaponSlot;

    [Header("��� ���� �� ������ �̹���")]
    [SerializeField] private Sprite emptySlotSprite;

    // ���� �� ��� ����Ʈ
    [SerializeField] List<InventoryItem> _equipItems;

    // �κ��丮 ���� ����
    [field: SerializeField] public int Size { get; private set; } = 4;

    List<UIInventoryItem> _listOfUIEquip = new List<UIInventoryItem>();    // ���� ������ �� ������ UI_Progress ���� ����Ʈ

    public void Initialize()
    {
        UpdateAllSlots();
    }

    /// <summary>
    /// ��� ������ ������Ʈ
    /// </summary>
    public void UpdateAllSlots()
    {
        UpdateSlot(EquipmentType.Helmet, helmetSlot);
        UpdateSlot(EquipmentType.Armor, armorSlot);
        UpdateSlot(EquipmentType.Back, backSlot);
        UpdateSlot(EquipmentType.Weapon, weaponSlot);
    }

    /// <summary>
    /// �� ������ ���������� ������Ʈ
    /// </summary>
    private void UpdateSlot(EquipmentType type, Image image)
    {
        var item = _equipManager.GetEquippedItem(type);
        if (item != null && item.ItemImage != null)
        {
            image.sprite = item.ItemImage;
            /*image.color = Color.white;*/
        }
        else
        {
            image.sprite = emptySlotSprite;
            image.color = new Color(1, 1, 1, 0.4f); // ������ ó��
        }
    }

   
  
    public void InitInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(_prefab, Vector2.zero, Quaternion.identity);
            uiItem.transform.SetParent(_parentTransform);
            uiItem.transform.localScale = Vector2.one;
            _listOfUIEquip.Add(uiItem);
        }
    }

    public void Init()
    {
        // ���� �� ��񸮽�Ʈ�� ���� ����
        _equipItems = new List<InventoryItem>();

        // ���� ������ŭ ��� �ִ� ���������� ä��
        for (int i = 0; i < Size; i++)
        {
            _equipItems.Add(InventoryItem.GetEmptyItem());
        }
    }
}
