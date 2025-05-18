using Inventory.Model;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Inventory.View;

namespace Inventory
{
    /// <summary>
    /// InventoryPage(UI)�� InventorySO(������ ��)�� �߰��ϴ� ��Ʈ�ѷ� ����
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] UIInventoryPage _inventoryUI;       // UI
                                                          
        [SerializeField] InventorySO _inventoryData;         // Data

        private void Start()
        {
            PrepareUI();
            /* _inventoryData.Init();*/
        }
        private void PrepareUI()
        {
            _inventoryUI.InityInventoryUI(_inventoryData.Size);               // UI ���� ���� �ʱ�ȭ

            _inventoryUI.OnDescriptionRequested += RequestDescription;  // ���� ��û �̺�Ʈ ����
            _inventoryUI.OnSwapItems += HandleSwapItems;                      // ������ ��ȯ �̺�Ʈ ����
            _inventoryUI.OnStartDragging += HandleDragging;                   // �巡�� ���� �� �̺�Ʈ ����
            _inventoryUI.OnItemActionRequested += HandleItemActionRequest;    // ������ �׼� ��û �̺�Ʈ ����
        }

        /// <summary>
        /// �κ��丮 ����/�ݱ� �޼���
        /// �κ��丮 ��ư�� ����
        /// </summary>
        public void OnInventory()
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
        private void RequestDescription(int itemIndex)
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
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            _inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        /// <summary>
        /// �巡�� ���� �� ó��
        /// �巡���ϴ� �������� �̹����� ���� ������ MouserFollwer�� �Ȱ��� ����
        /// </summary>
        /// <param name="itemIndex"></param>
        private void HandleDragging(int itemIndex)
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
        private void HandleItemActionRequest(int itemIndex)
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