using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.View
{
    /// <summary>
    /// �κ��丮 �� ������ UI_Progress �� ĭ�� �����ϴ� Ŭ����
    // Ŭ��, �巡��, ��� �� ���콺 �̺�Ʈ�� ó��
    /// </summary>
    public class UIInventoryItem :
        MonoBehaviour,
        IPointerClickHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IDropHandler,
        IDragHandler
    {
        [SerializeField] Image _itemImage; // ������ �̹���

        [SerializeField] TMP_Text _quantityText; // ���� ����

        [SerializeField] Image _borderImage; //������ ���� ǥ�� �׵θ�

        private bool _empty = true; // �� ĭ�� ����ִ��� ����

        // �ܺο��� ������ �� �ִ� �̺�Ʈ ���� (������ Ŭ��, �巡�� ����/��, ���, ��Ŭ�� ��)
        public event Action<UIInventoryItem>
            OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseButtonClick;

        public void Awake()
        {
            ResetData();           
            Deselect();            
        }

        /// <summary>
        /// ������ �����͸� �ʱ�ȭ�Ѵ�.
        /// ����� ������ �̹��� ����� ����ִ� ���·� ǥ��.
        /// </summary>
        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            _empty = true;
        }

        /// <summary>
        /// ������ ���� ��, �׵θ� Ȱ��ȭ�Ͽ� ���� ���� ǥ��
        /// </summary>
        public void Select()
        {
            _borderImage.enabled = true;
        }

        /// <summary>
        /// ������ ���� ��� ��, ������ ��Ȱ��ȭ
        /// </summary>
        public void Deselect()
        {
            _borderImage.enabled = false;
        }

        /// <summary>
        /// ������ �̹��� �� ���� UI�� ������ ����
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="quantity"></param>
        public void SetData(Sprite sprite, int quantity)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;
            _quantityText.text = quantity + "";
            _empty = false;
        }

        #region ���콺 �̺�Ʈ �ڵ鷯
        /// <summary>
        /// Ŭ��/��ġ ó��
        /// </summary>
        /// <param name="pointerData"></param>
        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                // ��Ŭ�� �� ��Ŭ�� �̺�Ʈ �߻�
                OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                // ��Ŭ�� �� �Ϲ� Ŭ�� �� �̺�Ʈ �߻�
                OnItemClicked?.Invoke(this);
            }
        }

        /// <summary>
        /// �巡�� ���� �� ȣ��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            // �� �����̸� return
            if (_empty)
                return;

            OnItemBeginDrag?.Invoke(this);
        }

        /// <summary>
        /// �巡�� ���� �� ȣ��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        /// <summary>
        /// �ٸ� ���� ���� ������� �� ȣ��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        /// <summary>
        /// �巡�� �� ���콺 �̵� ��  ȣ��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {

        }
        #endregion
    }
}