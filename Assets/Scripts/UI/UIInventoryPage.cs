using Inventory.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.View
{
    // UI �� InventoryController �� InventorySO �� UI �� �帧���� �����̸�,
    // �̺�Ʈ�� ���� UI�� ������ �� �� ������ ��û, ����, ������ �̷�����ϴ�.

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
        [SerializeField] UIInventoryItem _itemPrefab;

        [SerializeField] RectTransform _contentPanel;

        [SerializeField] UIInventoryDescription _itemDescription;

        [SerializeField] MouseFollower _mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();    // ���� ������ �� ������ UI ���� ����Ʈ

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
        public void InityInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, Vector2.zero, Quaternion.identity);
                uiItem.transform.SetParent(_contentPanel);
                uiItem.transform.localScale = Vector2.one;
                listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += SelectItem;
                uiItem.OnItemBeginDrag += BegingDrag;
                uiItem.OnItemDroppedOn += Swap;
                uiItem.OnItemEndDrag += EndDrag;
                uiItem.OnRightMouseButtonClick += ShowItemActions;
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
            foreach (UIInventoryItem item in listOfUIItems)
            {
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
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
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
            listOfUIItems[itemIndex].Select();
        }

        #region Execute Method
        /// <summary>
        /// ������ ���� ���� �� ȣ��
        /// ������ ������ �ε����� ã�� ������ ���� ��û
        /// </summary>
        /// <param name="item"></param>
        public void SelectItem(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
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
        private void BegingDrag(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
                return;

            curDraggedIndex = index;
            SelectItem(item);
            OnStartDragging?.Invoke(index);
        }

        

        /// <summary>
        /// ������ �巡�� �� ����� ���� �ڸ��ٲ� �̺�Ʈ �߻��ϴ� �޼���
        /// </summary>
        /// <param name="item"></param>
        private void Swap(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(curDraggedIndex, index);
        }


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

        private void EndDrag(UIInventoryItem item)
        {
            ResetDraggedItem();
        }

        /// <summary>
        /// ������ ��Ŭ�� �׼� ��û �� �̺�Ʈ �߻�
        /// ��Ŭ�� ��� ���� ����
        /// </summary>
        /// <param name="item"></param>
        private void ShowItemActions(UIInventoryItem item)
        {
            int index = listOfUIItems.IndexOf(item);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }
        #endregion
    }
}