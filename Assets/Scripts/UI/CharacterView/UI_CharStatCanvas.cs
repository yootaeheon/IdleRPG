using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharStatCanvas : MonoBehaviour
{
    /// <summary>
    /// ����â Canvas UI_Progress Ȱ��ȭ
    /// </summary>
    public void Button_Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ����â Canvas UI_Progress ��Ȱ��ȭ
    /// </summary>
    public void Button_Hide()
    {
        gameObject.SetActive(false);
    }
}
