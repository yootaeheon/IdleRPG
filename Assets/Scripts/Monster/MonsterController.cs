using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum MonsterState { Idle, Move, Detect, Attack }

public class MonsterController : Monster, IDamageable
{
    [Header("")]
    public Monster _model;

    private Transform character;

    private SpriteRenderer _spriteRenderer;

    private Color _originColor;

    private MonsterState currentState = MonsterState.Idle;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originColor = _spriteRenderer.color;
    }

    void Update()
    {
        switch (currentState)
        {
            case MonsterState.Idle:
                Move();
                SearchForEnemies();
                break;

            case MonsterState.Move:
                Move();
                SearchForEnemies();
                break;

            case MonsterState.Detect:
                if (character != null)
                    StartCoroutine(AttackEnemy());
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

    IEnumerator AttackEnemy()
    {
        currentState = MonsterState.Attack;

        while (character != null)
        {
            character.GetComponent<IDamageable>().TakeDamage(_model.AttackDamage);

            yield return new WaitForSeconds(_model.AttackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = MonsterState.Idle;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.AttackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log($"{Damage}�� ���ظ� ĳ���Ϳ��� �־���!");
        transform.DOShakePosition(0.3f, 0.2f);
        _spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(()=> _spriteRenderer.DOColor(_originColor, 0.1f));

    }
}
