using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CameraUtil
{
    /// <summary>
    /// ī�޶� ��ο����� FadeOut ���
    /// ī�޶� ��ο����� ������� FadeIn ���
    /// </summary>
    public static void CameraFadeIn()
    {
        Camera cam = Camera.main;

        cam.GetComponent<Animator>().SetTrigger("FadeOut");
    }
}
