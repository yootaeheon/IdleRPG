using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopCanvas : MonoBehaviour
{
    /// <summary>
    /// ���� Canvas UI_Progress Ȱ��ȭ
    /// </summary>
    public void Button_Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���� Canvas UI_Progress ��Ȱ��ȭ
    /// </summary>
    public void Button_Hide()
    {
        gameObject.SetActive(false);
    }
}
