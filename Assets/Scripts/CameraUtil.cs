using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CameraUtil
{
    /// <summary>
    /// ī�޶� ��ο����� ������� FadeIn ���
    /// </summary>
    public static void CameraFadeIn()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("FadeIn");
    }


    /// <summary>
    /// ī�޶� ��ο����� FadeOut ���
    /// </summary>
    public static void CameraFadeOut()
    {
        Camera.main.GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
