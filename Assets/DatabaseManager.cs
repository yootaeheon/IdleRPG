using Firebase.Database;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    public UserGameDataDTO GameData;

    public DatabaseReference userDataRef { get; private set; }


    [SerializeField] CharacterModel _model;
    public CharacterModel Model => _model ??= FindAnyObjectByType<CharacterModel>();

    [SerializeField] ProgressSO _progressData;
    public ProgressSO ProgressData => _progressData ??= FindAnyObjectByType<ChapterManager>()?.ProgressInfo;


    public bool IsGameDataLoaded { get; private set; }
    public Action OnGameDataLoaded { get; set; }

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        if (BackendManager.Instance.OnFirebaseReady)
        {
            LoadAllGameData();
        }
    }

    #region �̱��� ����
    public void SetSingleton()
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
    #endregion

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveAllGameData();
        }
    }

    public void SaveAllGameData()
    {
        string userId = BackendManager.Auth?.CurrentUser?.UserId;
        if (string.IsNullOrEmpty(userId)) return;

        if (Model == null)
        {
            Debug.LogWarning("SaveAllGameData ����: _model�� null�Դϴ�.");
            return;
        }

        if (ProgressData == null)
        {
            Debug.LogWarning("SaveAllGameData ����: _progressData�� null�Դϴ�.");
            return;
        }

        userDataRef = BackendManager.Database.GetReference("users").Child(userId);

        CharacterModelDTO characterDTO = new CharacterModelDTO
        (
            Model.MaxHp,
            Model.RecoverHpPerSecond,
            Model.DefensePower,
            Model.AttackPower,
            Model.AttackSpeed,
            Model.CriticalChance
        );

        ProgressDataDTO progressDTO = new ProgressDataDTO
        (
            ProgressData.Chapter,
            ProgressData.Stage,
            ProgressData.KillCount
        );

        GameData = new UserGameDataDTO(characterDTO, progressDTO);
        string json = JsonUtility.ToJson(GameData);

        userDataRef.Child("gameData")
            .SetRawJsonValueAsync(json)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("��� ���� ������ ���� �Ϸ�!");
                else
                    Debug.LogWarning("���� ������ ���� ����: " + task.Exception);
            });
    }

    #region ��� ������ ������Ʈ
    public void LoadAllGameData()
    {
        Debug.Log("LOAD ALL GAMEDATA 1");
        string uid = BackendManager.Auth?.CurrentUser?.UserId;
        Debug.Log("LOAD ALL GAMEDATA 2");
        if (string.IsNullOrEmpty(uid)) return;
        Debug.Log("LOAD ALL GAMEDATA 3");

        BackendManager.Database
            .GetReference("users").Child(uid).Child("gameData")
            .GetValueAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    if (_model == null || _progressData == null) return;

                    string json = task.Result.GetRawJsonValue();
                    GameData = JsonUtility.FromJson<UserGameDataDTO>(json);


                    Model.MaxHp = GameData.CharacterModelDTO.MaxHp;
                    Model.RecoverHpPerSecond = GameData.CharacterModelDTO.RecoverHpPerSecond;
                    Model.DefensePower = GameData.CharacterModelDTO.DefensePower;
                    Model.AttackPower = GameData.CharacterModelDTO.AttackPower;
                    Model.AttackSpeed = GameData.CharacterModelDTO.AttackSpeed;
                    Model.CriticalChance = GameData.CharacterModelDTO.CriticalChance;

                    ProgressData.Chapter = GameData.ProgressDataDTO.Chapter;
                    ProgressData.Stage = GameData.ProgressDataDTO.Stage;
                    ProgressData.KillCount = GameData.ProgressDataDTO.KillCount;

                    IsGameDataLoaded = true;
                    OnGameDataLoaded.Invoke();


                    Debug.Log("��� ���� ������ �ҷ����� �Ϸ�!");
                }
                else
                {
                    Debug.LogWarning("���� ������ �ҷ����� ����: " + task.Exception);
                }
            });
    }

    #endregion
}