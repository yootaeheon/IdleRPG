using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public enum MonsterState { Idle, Move, Detect, Attack }

public class MonsterController : Monster, IDamageable
{
    [Header("")]
    public Monster _model;

    private Transform character;

    private SpriteRenderer _spriteRenderer;

    private PooledObject _pooledObject;

    private Color _originColor;

    private MonsterState currentState = MonsterState.Move;

    private CharacterController _characterController;

    private MonsterSpawner _spawner;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originColor = _spriteRenderer.color;
        _pooledObject = GetComponent<PooledObject>();
        _characterController = FindAnyObjectByType<CharacterController>();
        _spawner = FindObjectOfType<MonsterSpawner>();
    }

    private void OnEnable()
    {
        IsDead = false;
        CurHp = MaxHp;
        currentState = MonsterState.Move;
    }

    private void OnDisable()
    {
        IsDead = true;
    }

    void Update()
    {
        switch (currentState)
        {
            case MonsterState.Idle:
                SearchForEnemies();
                break;

            case MonsterState.Move:
                Move();
                SearchForEnemies();
                break;

            case MonsterState.Detect:
                if (character != null)
                    StartCoroutine(AttackRoutine());
                break;
        }
    }

    void SearchForEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, _model.AttackRange, _model.EnemyLayer);
        if (enemy != null)
        {
            character = enemy.transform;
            currentState = MonsterState.Detect;
        }
    }

    void Move()
    {
        // �ܼ� �̵� ���� (��: �÷��̾� �Է�, AI �̵� ����)
        transform.Translate(Vector2.left * _model.MoveSpeed * Time.deltaTime);
    }

    IEnumerator AttackRoutine()
    {
        currentState = MonsterState.Attack;

        while (character != null)
        {
            Attack();

            yield return Util.GetDelay(_model.AttackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = MonsterState.Move;
    }

    public void Attack()
    {
        character.GetComponent<IDamageable>().TakeDamage(_model.AttackDamage);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.AttackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        _model.CurHp -= Damage;
        Debug.Log($"{Damage}��ŭ ���� ����");

        transform.DOShakePosition(0.3f, 0.2f);

        _spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(()=> _spriteRenderer.DOColor(_originColor, 0.1f));

        if (_model.CurHp <= 0)
        {
            CurHp = 0;
            Die();
        }
    }

    void Die()
    {
        // DoTween�� ����Ͽ� ���Ͱ� ������� �ִϸ��̼�
        Sequence deathSequence = DOTween.Sequence();

        // ���̵�ƿ�(����ȭ)
        deathSequence.Append(_spriteRenderer.DOFade(0, 0.5f));

        // ũ�� ���
        deathSequence.Join(transform.DOScale(Vector3.zero, 0.5f));

        // �ִϸ��̼� �Ϸ� �� ��Ȱ��ȭ �� ���ʱ�ȭ
        deathSequence.OnComplete(() =>
        {
            _pooledObject.CallReturnPool();
            _characterController._monster = null;
            transform.localScale = Vector3.one;
            _spriteRenderer.DOFade(1, 0f);
            _spawner.Spawn();
        });
    }
}
