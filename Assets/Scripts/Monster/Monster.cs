using System;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour
{
    // Monster Status ����
    // MaxHp = BaseHp �� (1 + (Chapter - 1) �� 0.2 + (Stage - 1) �� 0.05);
    // AttackDamage = BaseDamage �� (1 + (Chapter - 1) �� 0.2 + (Stage - 1) �� 0.05);
    // é�Ͱ� �ö󰡸� (Chapter - 1) * 0.2��ŭ, ���������� �ö󰡸� (Stage - 1) * 0.05��ŭ ���̵��� ����

    public DataManager Data;
    private int _curChapter => ChapterManager.Instance.ProgressInfo.Chapter;
    private int _curStage => ChapterManager.Instance.ProgressInfo.Stage;


    private void Start() => InitStatus(_curChapter, _curStage);

    private void OnEnable()
    {
        //  ProgressInfo.Stage, chpater ���������� é�� ���� �� ȣ���� �̺�Ʈ�� InitStatus(_curChapter, _curStage); ����

    }

    private void OnDisable()
    {
        //  ProgressInfo.Stage, chpater ���������� é�� ���� �� ȣ���� �̺�Ʈ��  InitStatus(_curChapter, _curStage); ���� ����
    }

    /// <summary>
    /// Stage�� �´� ���� ���� �ʱ�ȭ
    /// �޼����� �Ű������� stageNum
    /// </summary>
    /// <param name="stageNum"></param>
    public void InitStatus(int chapter, int stage, float baseHp = 100f, float baseDamage = 10f)
    {
        float multiplier = 1f + (chapter - 1) * 0.2f + (stage - 1) * 0.05f;

        MaxHp = Mathf.Round(baseHp * multiplier * 10f) / 10f;               // �Ҽ� ù° �ڸ�����
        AttackDamage = Mathf.Round(baseDamage * multiplier * 10f) / 10f;

        CurHp = MaxHp; // ü�� �ʱ�ȭ
    }


    [Header("Monster Status")]
    [SerializeField] bool _isDead;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    [SerializeField] float _curHp;
    public float CurHp { get { return _curHp; } set { _curHp = value; OnCurHpChanged?.Invoke(); } }
    public Action OnCurHpChanged;

    [SerializeField] float _maxHp;
    public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }


    [Header("TempAttack")]
    [SerializeField] LayerMask _enemyLayer;
    public LayerMask EnemyLayer { get { return _enemyLayer; } set { _enemyLayer = value; } }

    [SerializeField] float _attackRange;
    public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }

    [SerializeField] float _attackDamage;
    public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }

    [SerializeField] float _attackInterval;
    public float AttackInterval { get { return _attackInterval; } set { _attackInterval = value; } }


    public UI_HealthBar _healthBar;

}
