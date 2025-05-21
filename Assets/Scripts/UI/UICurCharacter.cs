using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICurCharacter : MonoBehaviour
{
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
    /// ����â UI Ȱ��ȭ
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ����â UI ��Ȱ��ȭ
    /// </summary>
    public void Hide()
    {
        gameObject?.SetActive(false);
    }
}
