using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using System;

public enum CharacterState { Idle, Move, Detect, Attack }

public class CharacterController : MonoBehaviour, IDamageable
{
    public CharacterModel _model;

    private Transform _monster;

    private CharacterState _currentState = CharacterState.Move;

    private Animator _animator;

    public Action OnEncounterMonster { get; set; }       // ���� ���� ��, �߻��ϴ� �̺�Ʈ (�� ��ũ�Ѹ� ����)
    public Action OnKillMonster { get; set; }                 // ���� óġ ��, �߻��ϴ� �̺�Ʈ (�� ��ũ�Ѹ� ����)

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (_currentState)
        {
            case CharacterState.Idle:
                _animator.SetBool("1_Move", false);
                SearchForEnemies();
                break;

            case CharacterState.Move:
                _animator.SetBool("1_Move", true);
                SearchForEnemies();
                break;

            case CharacterState.Detect:
                if (_monster != null)
                {
                    _animator.SetBool("1_Move", false);
                    StartCoroutine(AttackRoutine());
                }
                break;
        }
    }

    void SearchForEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, _model.AttackRange, _model.EnemyLayer);
        if (enemy != null)
        {
            _monster = enemy.transform;
            _currentState = CharacterState.Detect;
            OnEncounterMonster?.Invoke();
        }
    }

    IEnumerator AttackRoutine()
    {
        _currentState = CharacterState.Attack;

        while (_monster != null)
        {
            _animator.SetTrigger("2_Attack");

            yield return new WaitForSeconds(_model.AttackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        _currentState = CharacterState.Move;
        OnKillMonster?.Invoke();
    }

    public void Attack()
    {
        _monster.GetComponent<IDamageable>().TakeDamage(_model.AttackDamage);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _model.AttackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        _model.CurHp -= Damage;

        transform.DOShakePosition(0.2f, 0.1f);
    }
}
