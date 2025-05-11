using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private bool _empty = true;

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
    /// ������ ����;
    /// ����� ������ �̹����� ��Ȱ��ȭ�Ͽ� �Ͼ�� ���̰� ����;
    /// �߰��� �� Ŀ���� ����;
    /// </summary>
    public void ResetData()
    {
        this._itemImage.gameObject.SetActive(false);
        _empty = true;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void Select()
    {
        _borderImage.enabled = true;
    }

    /// <summary>
    /// ������ ���� ���
    /// </summary>
    public void Deselect()
    {
        _borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this._itemImage.gameObject.SetActive(true);
        this._itemImage.sprite = sprite;
        this._quantityText.text = quantity + "";
        _empty = false;
    }

    #region Call Event Method
    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_empty)
            return;

        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);

    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
    #endregion
}
