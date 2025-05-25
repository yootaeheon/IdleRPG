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

    private void OnEnable()
    {
        ProgressInfo.OnClearStage += ClearStage;
    }

    private void OnDisable()
    {
        ProgressInfo.OnClearStage -= ClearStage;
    }

    /// <summary>
    /// Stage�� �´� ���� ���� �ʱ�ȭ
    /// �޼����� �Ű������� stageNum
    /// </summary>
    /// <param name="stageNum"></param>
    /*private void Init(int chapter, int stage)
    {
        ProgressInfo.Stage = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.Stage));
    }*/


    /// <summary>
    /// é�� Ŭ���� ��, ���� é�ͷ�
    /// FadiIn ȿ��
    /// </summary>
    public void ClearChapter()
    {
        ProgressInfo.Chapter++;
        ProgressInfo.Stage = 1;

        CameraUtil.CameraFadeIn();
        DelayLayerMoveSpeed();
    }

    /// <summary>
    /// é�� �� �������� Ŭ���� ��, ���� ����������
    /// FadeIn ȿ��
    /// </summary>
    public void ClearStage()
    {
        ProgressInfo.Stage++;

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