using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

/// <summary>
/// InventoryPage�� InventorySO�� �߰�����;
/// </summary>
public class InventoryController : MonoBehaviour
{
    [SerializeField] UIInventoryPage _inventoryUI;

    [SerializeField] InventorySO _inventoryData;

    private void Start()
    {
        PrepareUI();
        /* _inventoryData.Init();*/
    }

    public void OnInventory()
    {
        if (_inventoryUI.isActiveAndEnabled == false)
        {
            _inventoryUI.Show();
            foreach (var item in _inventoryData.GetCurInventoryState())
            {
                _inventoryUI.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Quantity);
            }
        }
        else
        {
            _inventoryUI.Hide();
        }
    }

    private void PrepareUI()
    {
        _inventoryUI.InityInventoryUI(_inventoryData.Size);
        this._inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this._inventoryUI.OnSwapItems += HandleSwapItems;
        this._inventoryUI.OnStartDragging += HandleDragging;
        this._inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    #region InventoryUI�� �����Ͽ� �� ������ ����
    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            _inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.Item;
        _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }
    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
       
    }

    private void HandleDragging(int itemIndex)
    {
        
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            _inventoryUI.ResetSelection();
        }

        ItemSO item = inventoryItem.Item;
        _inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);

    }
    #endregion

}
