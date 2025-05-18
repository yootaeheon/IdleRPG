using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        // �κ��丮�� ���� �����۵��� ����Ʈ
        [SerializeField] List<InventoryItem> _inventoryItems;

        // �κ��丮 ���� ����
        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

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

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Quantity);
        }

        /// <summary>
        /// �������� �κ��丮�� �߰��ϴ� �Լ�
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public int AddItem(ItemSO item, int quantity)
        {
            if (item.IsStackable == false)
            {
                // �κ��丮�� �� ������ Ȯ��
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstSlot(item, 1);
                    }
                    return quantity;

                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstSlot(ItemSO item, int quantity)
        {
            InventoryItem newItem = new InventoryItem()
            {
                Item = item,
                Quantity = quantity
            };

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        /// <summary>
        /// �κ��丮�� ����á���� Ȯ��
        /// </summary>
        /// <returns></returns>
        private bool IsInventoryFull()
            => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;

                if (_inventoryItems[i].Item.ID == item.ID)
                {
                    int amountPossibleToTake = _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].Quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstSlot(item, newQuantity);
            }
            return quantity;
        }

       

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurInventoryDic());
        }

        /// <summary>
        /// ���� �κ��丮 ���¸� Dictionary ���·� ��ȯ (�� ������ ����)
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, InventoryItem> GetCurInventoryDic()
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

        public InventoryItem GetItemIndex(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        public void RemoveItem(int itemIndex, int amount)
        {
            if (_inventoryItems.Count > itemIndex)
            {
                if (_inventoryItems[itemIndex].IsEmpty)
                    return;

                int reminder = _inventoryItems[itemIndex].Quantity - amount;
                if (reminder <= 0)
                {
                    _inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    _inventoryItems[itemIndex] = _inventoryItems[itemIndex].ChangeQuantity(reminder);
                } 

                InformAboutChange();
            }
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItem item1 = _inventoryItems[itemIndex_1];
            _inventoryItems[itemIndex_1] = _inventoryItems[itemIndex_2];
            _inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
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
}