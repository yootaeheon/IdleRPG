using CustomUtility.IO;
using UnityEngine;

/// <summary>
/// CSV Data�� �а� �����س��� Ŭ����
/// CSV �����͸� ���� �ʿ䰡 ���� �� DataManager���� MonsterCSV�� �ҷ��ͼ� GetData()ȣ���ϸ� ��
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager Instance {  get; private set; }
    [field : SerializeField] public CsvTable MonsterCSV { get; private set; }

    private void Awake() => Init();
    private void Init()
    {
        SetSingleton();
        CsvReader.Read(MonsterCSV);
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
}

public enum MonsterData
{
    Chapter_Stage,
    Chapter,
    Stage,
    MaxHp,
    AttackDamage,
}