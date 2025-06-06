using Inventory.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Item.ElementTypeEnum;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] CharacterModel Model;

    [SerializeField] InventorySO InventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                int reminder = InventoryData.AddItem(item.InventoryItem, item.Quantity);

                if (reminder == 0)
                {
                    item.DestroyItem();
                }
                else
                {
                    item.Quantity = reminder;
                }
            }

          
            if (item.InventoryItem is ConsumeItemSO consumeItem)
            {
                AddElement(consumeItem.ElementType);
            }
        }
    }

    public void AddElement(ElementType type)
    {
        Model.ElementType = type;
        Console.WriteLine($"�Ӽ� �߰���: {type}, ���� �Ӽ�: {Model.ElementType}");
    }

    // Ư�� �Ӽ��� ������ �ִ��� Ȯ��
    public bool HasElement(ElementType type)
    {
        return (Model.ElementType & type) != 0;
    }

    // �Ӽ� ���� (��: ���ֹ��� ������ ��)
    public void RemoveElement(ElementType type)
    {
        Model.ElementType &= ~type;
        Console.WriteLine($"�Ӽ� ���ŵ�: {type}, ���� �Ӽ�: {Model.ElementType}");
    }
}
