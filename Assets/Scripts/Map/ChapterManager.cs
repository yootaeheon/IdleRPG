using System.Collections;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance { get; private set; }

    [SerializeField] ParallaxBackground _backGround;

    [SerializeField] CharacterController _characterController;

    public StageInfo[] StageInfos { get; private set; }


    private void Awake()
    {
        SetSingleton();
    }

    /// <summary>
    /// �������� Ŭ���� �� FadeIn �ʿ� ���� �� ���� ������ ��
    /// </summary>
   /* private void OnEnable()
    {
        _characterController.OnKillMonster += ClearStage;
    }

    private void OnDisable()
    {
        _characterController.OnKillMonster -= ClearStage;
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("1");
            ClearChapter();
        }
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

    /// <summary>
    /// é�� Ŭ���� ��, ���� é�ͷ�
    /// </summary>
    public void ClearChapter()
    {
        CameraUtil.CameraFadeIn();
        DelayLayerMoveSpeed();
    }

    /// <summary>
    /// é�� �� �������� Ŭ���� ��, ���� ����������
    /// </summary>
    public void ClearStage()
    {
        CameraUtil.CameraFadeIn();
        DelayLayerMoveSpeed();
    }


    /// <summary>
    /// ������ �ְ� LayerMoveSpeed �ʱ�ȭ
    /// </summary>
    /// <returns></returns>
    Coroutine DelayLayerMoveSpeedRoutine;
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