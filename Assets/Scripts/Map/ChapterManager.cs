using System.Collections;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance { get; private set; }

    [SerializeField] ParallaxBackground _backGround;

    [SerializeField] CharacterController _characterController;

    public DataManager Data;

    [Header("")]
    [SerializeField] ProgressSO _progressInfo;
    public ProgressSO ProgressInfo { get { return _progressInfo; } set { _progressInfo = value; } }


    private void Awake() => SetSingleton();
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
    /// Stage�� �´� ���� ���� �ʱ�ȭ
    /// �޼����� �Ű������� stageNum
    /// </summary>
    /// <param name="stageNum"></param>
    private void Init(int stageNum)
    {
        ProgressInfo.MonsterNumInStage = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.Num));
    }


    /// <summary>
    /// é�� Ŭ���� ��, ���� é�ͷ�
    /// FadiIn ȿ��
    /// </summary>
    public void ClearChapter()
    {
        _progressInfo.Chapter++;
        _progressInfo.Stage = 1;

        CameraUtil.CameraFadeIn();
        DelayLayerMoveSpeed();
    }

    /// <summary>
    /// é�� �� �������� Ŭ���� ��, ���� ����������
    /// FadeIn ȿ��
    /// </summary>
    public void ClearStage()
    {
        _progressInfo.Stage++;

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


    // �������� ���
    // ���� ProgressSO�� ���� ���൵ ����
    // stage Ŭ���� �� curStage++; 
    // chapter Ŭ���� �� chapter++; curStage = 1; 
    // (1é�Ϳ� 10 ������������ ����)
    // Stage�� Chapter Ŭ���� �� FadeIn ȿ��
    // chapterManager���� �̺�Ʈ�� ������ �ְ� ���������� é�� Ŭ���� �� OnStageClear?.Invoke(); OnChapterClear?.Invoke();
    // Ŭ�����Ҷ����� MonsterData�� ������Ʈ�ؼ� ���Ϳ� �Ҿ�־���
    // �Ʒ� �ڵ带 �̿��Ͽ� Init(_progressInfo.stage); �̿��Ͽ� �̺�Ʈȣ���Ҷ� �Լ��� ȣ��
    // 
    /*private void Init(int stageNum)
    {
        ProgressInfo.MonsterNumInStage = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.Num));
    }*/
}