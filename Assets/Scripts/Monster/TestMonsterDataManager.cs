using CustomUtility.IO;
using UnityEngine;

public class TestMonsterDataManager : MonoBehaviour
{
    [field : SerializeField] public CsvTable MonsterCSV { get; private set; }

    private void Awake() => Init();

    private void Start() => TestMethod();

    private void Init()
    {
        CsvReader.Read(MonsterCSV);
    }

    private void TestMethod()
    {
        /* // 2 Stage�� ���͵��� MaxHp �ҷ�����
        Debug.Log(MonsterCSV.GetData(2, (int)MonsterData.MaxHp));*/
    }
}

public enum MonsterData
{
    Num = 1,
    MaxHp,
    AttackDamage,
}