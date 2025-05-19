using Inventory.Model;
using Inventory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.View
{
    // UIInventoryPage(UI) �� InventoryController �� InventorySO(Data) �� UIInventoryPage(UI) �� �帧���� �����̸�,
    // { Event - Handle - Call - Method }
    // �̺�Ʈ�� ���� UI�� ������ �� �� ������ ��û, ����, ������ �̷����

    /// <summary>
    /// �κ��丮 UI �� ������ ��ü ���� Ŭ����
    /// 1. ������ UI ���� ����
    /// 2. ����
    /// 3. �巡�� �� ���
    /// 4. ���� ǥ��
    /// 5. �׼� ��û
    /// </summary>
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] EquipmentManager _equipmentManager;

        [SerializeField] UIInventoryItem _itemPrefab;

        [SerializeField] RectTransform _contentPanel;

        [SerializeField] UIInventoryDescription _itemDescription;

        [SerializeField] MouseFollower _mouseFollower;

        List<UIInventoryItem> _listOfUIItems = new List<UIInventoryItem>();    // ���� ������ �� ������ UI ���� ����Ʈ

        private int curDraggedIndex = -1; // ���� �巡�� ���� ������ �ε���

        /// <summary>
        /// �ܺο��� ���� �ϴ� �̺�Ʈ
        /// </summary>
        public event Action<int>
            OnDescriptionRequested,    // ����� ���� ��û
            OnItemActionRequested,     // ������ �׼� ��û �� (��Ŭ�� �޴�) // ��Ŭ�� ��� ���� ����
            OnStartDragging;           // �巡�� ���� ��

        public event Action<int, int> OnSwapItems; // ������ ��ġ ���� ��û ��(�巡��-���)


        private void Awake()
        {
            Hide();
            _mouseFollower.Toggle(false);
            _itemDescription.ResetDescription();
        }

        /// <summary>
        /// �κ��丮 UI ���� �ʱ�ȭ �� ����
        /// </summary>
        /// <param name="inventorySize"></param>
        public void InitInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector2.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                uiItem.transform.localScale = Vector2.one;
                _listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleSelectItem;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        /// <summary>
        /// �κ��丮 UI Ȱ��ȭ �� ���� �ʱ�ȭ
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
            ResetSelection();
        }

        /// <summary>
        /// �κ��丮 UI ��Ȱ��ȭ �� �巡�� ���� �ʱ�ȭ
        /// </summary>
        public void Hide()
        {
            gameObject?.SetActive(false);
            ResetDraggedItem();
        }

        /// <summary>
        /// ������ ���� �ʱ�ȭ �� ��� ������ ���� ����
        /// </summary>
        public void ResetSelection()
        {
            _itemDescription.ResetDescription();
            DeselectAllItems();
        }

        /// <summary>
        /// ��� ������ UI ������ ���� ���� ó��
        /// </summary>
        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in _listOfUIItems)
            {
                item.Deselect();
            }
        }

        internal void ResetAllItems()
        {
            foreach (var item in _listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        /// <summary>
        /// Ư�� �ε����� ������ UI �����͸� �����Ѵ�.
        /// </summary>
        /// <param name="itemIndex">������ �ε���</param>
        /// <param name="itemImage">������ �̹���</param>
        /// <param name="itemQuantity">������ ����</param>
        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listOfUIItems.Count > itemIndex)
            {
                _listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        /// <summary>
        /// Ư�� �ε��� ������ ���� UI ����
        /// </summary>
        /// <param name="itemIndex"></param>
        /// <param name="itemImage"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            _itemDescription.SetDescription(itemImage, name, description);
            DeselectAllItems();
            _listOfUIItems[itemIndex].Select();
        }

        /// <summary>
        /// ȣ��Ǵ� ���� ��ư �޼���
        /// </summary>
        /// <param name="selectedItem"></param>
        public void OnEquipButtonClicked(EquipItemSO selectedItem)
        {
            _equipmentManager.EquipItem(selectedItem);
        }

        #region Execute Method
        /// <summary>
        /// ������ ���� ���� �� ȣ��
        /// ������ ������ �ε����� ã�� ������ ���� ��û
        /// </summary>
        /// <param name="item"></param>
        public void HandleSelectItem(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
                return;

            OnDescriptionRequested?.Invoke(index);
        }

        /// <summary>
        /// ������ �巡�� ���� �� ȣ��
        /// ���� �巡�� ���� ������ �ε��� ���� ��,
        /// ���� ��û�ϰ�, 
        /// �巡�� �� �̺�Ʈ �߻�
        /// </summary>
        /// <param name="item"></param>
        private void HandleBeginDrag(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
                return;

            curDraggedIndex = index;
            HandleSelectItem(item);
            OnStartDragging?.Invoke(index);
        }

        /// <summary>
        /// ������ �巡�� �� ����� ���� �ڸ��ٲ� �̺�Ʈ �߻��ϴ� �޼���
        /// </summary>
        /// <param name="item"></param>
        private void HandleSwap(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
          
            OnSwapItems?.Invoke(curDraggedIndex, index);
            HandleSelectItem(item);
        }

        private void HandleEndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }



        /// <summary>
        /// ������ ��Ŭ�� �׼� ��û �� �̺�Ʈ �߻�
        /// ��Ŭ�� ��� ���� ����
        /// </summary>
        /// <param name="item"></param>
        private void HandleShowItemActions(UIInventoryItem item)
        {
            int index = _listOfUIItems.IndexOf(item);
            if (index == -1)
                return;
            
            OnItemActionRequested?.Invoke(index);
        }
        #endregion

        #region ���콺 �ȷο�
        /// <summary>
        /// MouseFollower Ȱ��ȭ ��,
        /// ���콺 ��ġ�� �Ȱ��� ����
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="quantity"></param>
        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(sprite, quantity);
        }


        /// <summary>
        /// MouseFollower ��Ȱ�� ȭ ��,
        /// ���� ���� ������ �ε��� -1�� ����
        /// </summary>
        private void ResetDraggedItem()
        {
            _mouseFollower.Toggle(false);
            curDraggedIndex = -1;
        }
    }
    #endregion
}