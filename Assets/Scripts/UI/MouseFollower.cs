using Inventory.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �巡�� �� ��� �� ������ UI �������� ���콺 ��ġ�� ����ְ�, �巡���� ������ ������ ������
/// </summary>
public class MouseFollower : MonoBehaviour
{
    [SerializeField] Canvas _canvas; // ����� ĵ����
    [SerializeField] UIInventoryItem _item; // �巡�� �� ǥ���� ������ UI

    private void Awake()
    {
        // �θ� ������Ʈ���� ĵ������ ã�� ����
        _canvas = transform.root.GetComponent<Canvas>();
        _item = GetComponentInChildren<UIInventoryItem>();

        // Overlay ��忡�� worldCamera �ʿ� ����
    }

    /// <summary>
    /// ������ ������ �����Ͽ� UI�� ǥ��
    /// </summary>
    public void SetData(Sprite sprite, int quantity)
    {
        _item.SetData(sprite, quantity);
    }

    /// <summary>
    /// Overlay ��忡���� ���콺 ��ġ�� ��ٷ� ��ġ�� ���� ����
    /// </summary>
    private void Update()
    {
        Vector2 screenPos = Input.mousePosition;

        // ���콺 ��ǥ�� ��ȿ���� ���� ��� ��� �ڵ�
        if (float.IsInfinity(screenPos.x) || float.IsInfinity(screenPos.y) ||
            float.IsNaN(screenPos.x) || float.IsNaN(screenPos.y))
        {
            return;
        }

        // RectTransform.position�� ���� ��ǥ���� Overlay���� ��ũ�� ��ǥ �״�� ��� ����
        transform.position = screenPos;
    }

    /// <summary>
    /// ���콺 ����ٴϴ� UI ǥ�� ���
    /// </summary>
    public void Toggle(bool value)
    {
        Debug.Log($"Item Toggled {value}");
        gameObject.SetActive(value);
    }
}
