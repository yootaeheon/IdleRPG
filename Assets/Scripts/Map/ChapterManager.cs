using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance {  get; private set; }

    [SerializeField] ParallaxBackground _backGround;

    public StageInfo[] StageInfos { get; private set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClearChapter()
    {
        //TODO : ȭ�� ��ο����� ������鼭 �ٽ� ���������� ���� �ִϸ��̼�
        // ĳ���� ������X ī�޶� ���� �� �ִϸ��̼� ��������
        // ȭ�� ������� �� �� �� ȭ�� ��ũ�Ѹ� ����
        DelayLayerMoveSpeed();
    }


    public void ClearStage()
    {
        //TODO : ȭ�� ��ο����� ������鼭 �ٽ� ���������� ���� �ִϸ��̼�
        // ĳ���� ������X ī�޶� ���� �� �ִϸ��̼� ��������
        // ȭ�� ������� �� �� �� ȭ�� ��ũ�Ѹ� ����
        DelayLayerMoveSpeed();
    }



    /// <summary>
    /// ������ �ְ� LayerMoveSpeed �ʱ�ȭ
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayLayerMoveSpeed()
    {
        yield return Util.GetDelay(2f);

        _backGround.SetLayerMoveSpeed();
    }
}

public class StageInfo
{
    public int Stage { get; private set; }
}