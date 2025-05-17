using Inventory.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �巡�� �� ��� �� ������UI�������� �����ϰ� ���콺 ��ġ�� ����ְ� �� UI�� �巡���� �������� �Ȱ��� ��������
/// </summary>
public class MouseFollower : MonoBehaviour
{
    [SerializeField] Canvas _canvas;

    [SerializeField] UIInventoryItem _item;

    private void Awake()
    {
        _canvas = transform.root.GetComponent<Canvas>();
        _item = GetComponentInChildren<UIInventoryItem>();

        // worldCamera�� null�� ��� Camera.main�� �Ҵ�
        if (_canvas.worldCamera == null) _canvas.worldCamera = Camera.main;
    }

    /// <summary>
    /// �巡���� �������� ���콺 ��ġ�� �Ȱ��� �ʱ�ȭ;
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="quantity"></param>
    public void SetData(Sprite sprite, int quantity)
    {
        _item.SetData(sprite, quantity);
    }

    /// <summary>
    /// ���콺 �����Ϳ� 2D ���� ĵ������ RectTransform ��ġ ��ȯ�� ����
    /// ����� ȯ��� �����ͻ󿡼� ���콺�� ��ġ�� ��� �������� �ʰų� �Ҵ���� �ʴ� ��ġ�� ���� �� ������ �߻��ϹǷ� ���� üũ����.
    /// </summary>
    private void Update()
    {
        if (_canvas.worldCamera == null) return;

        Vector2 screenPos = Input.mousePosition;

        // ���콺 ��ġ�� ��ȿ���� ���� ��� early return
        if (float.IsInfinity(screenPos.x) || float.IsInfinity(screenPos.y) ||
            float.IsNaN(screenPos.x) || float.IsNaN(screenPos.y))
        {
            return;
        }

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_canvas.transform,
                screenPos,
                _canvas.worldCamera,
                out pos))
        {
            transform.position = _canvas.transform.TransformPoint(pos);
        }
    }

    /// <summary>
    /// Toggle�� True�� ���� ���콺 ��ġ�� ������ �������� ����
    /// </summary>
    /// <param name="value"></param>
    public void Toggle(bool value)
    {
        Debug.Log($"Item Toggled {value}");
        gameObject.SetActive(value);
    }
}
