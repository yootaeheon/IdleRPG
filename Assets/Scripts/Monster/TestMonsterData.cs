using UnityEngine;

public class TestMonsterData : MonoBehaviour
{
    public TestMonsterDataManager Data;

    [Header("")]
    [SerializeField] int _curStageNum;

    [Header("")]
    [SerializeField] int _num;
    [SerializeField] int _maxHp;
    [SerializeField] int _attackDamage;


    private void Start() => Init(_curStageNum);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Init(_curStageNum);
        }
    }

    /// <summary>
    /// Stage�� �´� ���� ���� �ʱ�ȭ
    /// �޼����� �Ű������� stageNum
    /// </summary>
    /// <param name="stageNum"></param>
    private void Init(int stageNum)
    {
        _num = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.Num));
        _maxHp = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.MaxHp));
        _attackDamage = int.Parse(Data.MonsterCSV.GetData(stageNum, (int)MonsterData.AttackDamage));
    }
}
