using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharStatCanvas : MonoBehaviour
{
    // 현재 사용 X
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
    /// 스탯창 Canvas UI_Progress 활성화
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 스탯창 Canvas UI_Progress 비활성화
    /// </summary>
    public void Hide()
    {
        gameObject?.SetActive(false);
    }
}
