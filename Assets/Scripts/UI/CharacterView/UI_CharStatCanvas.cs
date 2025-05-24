using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharStatCanvas : MonoBehaviour
{
    // ���� ��� X
    public void OnOffCanvas()
    {
        if (gameObject.activeSelf == false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    /// <summary>
    /// ����â Canvas UI_Progress Ȱ��ȭ
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ����â Canvas UI_Progress ��Ȱ��ȭ
    /// </summary>
    public void Hide()
    {
        gameObject?.SetActive(false);
    }
}
