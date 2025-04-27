using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System;

public enum CharacterState { Idle, Move, Detect, Attack }

public class CharacterController : MonoBehaviour, IDamageable
{
    public CharacterModel _model;

    private Transform monster;

    private CharacterState currentState = CharacterState.Idle;

    public Action OnEncounterMonster;                                    // ���� ���� ��, �߻��ϴ� �̺�Ʈ (�� ��ũ�Ѹ� ����)
    public Action OnKillMonster;                                         // ���� óġ ��, �߻��ϴ� �̺�Ʈ (�� ��ũ�Ѹ� ����)


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
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, _model.AttackRange, _model.EnemyLayer);
        if (enemy != null)
        {
            monster = enemy.transform;
            currentState = CharacterState.Detect;
            OnEncounterMonster?.Invoke();
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
            monster.GetComponent<IDamageable>().TakeDamage(_model.AttackDamage);

            yield return new WaitForSeconds(_model.AttackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = CharacterState.Idle;
        OnKillMonster?.Invoke();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.AttackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        _model.CurHp -= Damage;

        transform.DOShakePosition(0.3f, 0.2f);
    }
}
