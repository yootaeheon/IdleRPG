using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading;

public enum CharacterState { Idle, Move, Detect, Attack }

public class CharacterController : MonoBehaviour, IDamageable
{
    public CharacterModel _model;

    private Transform monster;

    private CharacterState currentState = CharacterState.Idle;

    void Update()
    {
        switch (currentState)
        {
            case CharacterState.Idle:
                SearchForEnemies();
                break;

            case CharacterState.Move:
                Move();
                SearchForEnemies();
                break;

            case CharacterState.Detect:
                if (monster != null)
                    StartCoroutine(AttackEnemy());
                break;
        }
    }

    void SearchForEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, _model.attackRange, _model.enemyLayer);
        if (enemy != null)
        {
            monster = enemy.transform;
            currentState = CharacterState.Detect;
        }
    }

    void Move()
    {
        
    }

    IEnumerator AttackEnemy()
    {
        currentState = CharacterState.Attack;

        while (monster != null)
        {
            monster.GetComponent<IDamageable>().TakeDamage(_model.attackDamage);

            yield return new WaitForSeconds(_model.attackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = CharacterState.Idle;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.attackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log($"{Damage}�� ���ظ� ���Ϳ��� �־���!");
        transform.DOShakePosition(0.3f, 0.2f);
    }
}
