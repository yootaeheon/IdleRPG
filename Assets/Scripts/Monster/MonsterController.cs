using System.Collections;
using UnityEngine;
using DG.Tweening;

public enum MonsterState { Idle, Move, Detect, Attack }

public class MonsterController : MonoBehaviour, IDamageable
{
    public MonsterModel _model;

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
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, _model.attackRange, _model.enemyLayer);
        if (enemy != null)
        {
            character = enemy.transform;
            currentState = MonsterState.Detect;
        }
    }

    void Move()
    {
        // �ܼ� �̵� ���� (��: �÷��̾� �Է�, AI �̵� ����)
        transform.Translate(Vector2.left * _model.moveSpeed * Time.deltaTime);
    }

    IEnumerator AttackEnemy()
    {
        currentState = MonsterState.Attack;

        while (character != null)
        {
            character.GetComponent<IDamageable>().TakeDamage(_model.attackDamage);

            yield return new WaitForSeconds(_model.attackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = MonsterState.Idle;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.attackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log($"{Damage}�� ���ظ� ĳ���Ϳ��� �־���!");
        transform.DOShakePosition(0.3f, 0.2f);
        _spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(()=> _spriteRenderer.DOColor(_originColor, 0.1f));

    }
}
