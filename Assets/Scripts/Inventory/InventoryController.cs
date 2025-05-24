using Inventory.Model;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Inventory.View;
using System.Collections.Generic;

namespace Inventory
{
    /// <summary>
    /// InventoryPage(UI_Progress)�� InventorySO(������ ��)�� �߰��ϴ� ��Ʈ�ѷ� ����
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] UIInventoryPage _inventoryUI;       // UI_Progress
                                                          
        [SerializeField] InventorySO _inventoryData;         // Data

        public List<InventoryItem> _initItems = new List<InventoryItem>();

        private void Awake()
        {
            PrepareUI();
            /* _inventoryData.Init();*/
            PrepareInventoryData();
        }

        private void OnDisable()
        {
            _inventoryData.OnInventoryUpdated -= UpdateInventoryUI;

            _inventoryUI.OnDescriptionRequested -= CallRequestDescription;  // ���� ��û �̺�Ʈ ����
            _inventoryUI.OnSwapItems -= CallSwapItems;                      // ������ ��ȯ �̺�Ʈ ����
            _inventoryUI.OnStartDragging -= CallDragging;                   // �巡�� ���� �� �̺�Ʈ ����
            _inventoryUI.OnItemActionRequested -= CallItemActionRequest;    // ������ �׼� ��û �̺�Ʈ ����
        }

        private void PrepareUI()
        {
            _inventoryUI.InitInventoryUI(_inventoryData.Size);               // UI_Progress ���� ���� �ʱ�ȭ

            _inventoryUI.OnDescriptionRequested += CallRequestDescription;  // ���� ��û �̺�Ʈ ����
            _inventoryUI.OnSwapItems += CallSwapItems;                      // ������ ��ȯ �̺�Ʈ ����
            _inventoryUI.OnStartDragging += CallDragging;                   // �巡�� ���� �� �̺�Ʈ ����
            _inventoryUI.OnItemActionRequested += CallItemActionRequest;    // ������ �׼� ��û �̺�Ʈ ����
        }

        public void PrepareInventoryData()
        {
            _inventoryData.Init();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in _initItems)
            {
                if (item.IsEmpty)
                    continue;
                _inventoryData.AddItem(item);
            }
        }

        public void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
        }

        /// <summary>
        /// �κ��丮 ����/�ݱ� �޼���
        /// �κ��丮 ��ư�� ����
        /// </summary>
        public void OnOffInventory()
        {
            if (_inventoryUI.isActiveAndEnabled == false)
            {
                _inventoryUI.Show();
                foreach (var item in _inventoryData.GetCurInventoryDic())
                {
                    _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
                }
            }
            else
            {
                _inventoryUI.Hide();
            }
        }

       

        #region InventoryUI�� �����Ͽ� �� ������ ����
        /// <summary>
        /// ������ ���� ��û ó��
        /// �ش� �ε��� ������ ������ �����ͼ� UI�� ���� ������Ʈ
        /// �������� ������� ���� �ʱ�ȭ
        /// </summary>
        /// <param name="itemIndex"></param>
        private void CallRequestDescription(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemIndex(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.Item;
            _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        /// <summary>
        /// ������ ��ġ ��ȯ ó��
        /// InventorySO(������ ��)�� ������ ���� �޼��� ȣ��
        /// </summary>
        /// <param name="itemIndex_1"></param>
        /// <param name="itemIndex_2"></param>
        private void CallSwapItems(int itemIndex_1, int itemIndex_2)
        {
            _inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        /// <summary>
        /// �巡�� ���� �� ó��
        /// �巡���ϴ� �������� �̹����� ���� ������ MouserFollwer�� �Ȱ��� ����
        /// </summary>
        /// <param name="itemIndex"></param>
        private void CallDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemIndex(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryUI.CreateDraggedItem(inventoryItem.Item.ItemImage, inventoryItem.Quantity);
        }

        /// <summary>
        /// ������ ��� �� �׼� ��û ó��
        /// �������� ������� ��������
        /// 
        /// �׷��� ������ ������ ���� ������Ʈ �� ������ �׼� ȣ��
        /// �ʿ�� ������ �Ҹ� ó��
        /// </summary>
        /// <param name="itemIndex"></param>
        private void CallItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemIndex(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
            }

            ItemSO item = inventoryItem.Item;
            _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }
        #endregion
    }
}