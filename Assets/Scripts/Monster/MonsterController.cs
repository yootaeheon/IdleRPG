using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public enum MonsterState { Idle, Move, Detect, Attack }

public class MonsterController : Monster, IDamageable
{
    [Header("")]
    public Monster Base;
    public Transform character { get; set; }

    private SpriteRenderer _spriteRenderer;

    private PooledObject _pooledObject;

    private Color _originColor;

    private MonsterState currentState = MonsterState.Move;

    private CharacterController _characterController;

    private MonsterSpawner _spawner;

    [SerializeField] Animator _animator;

    [SerializeField] TMP_Text _damageText;

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
        currentState = MonsterState.Move;

        OnCurHpChanged += () => _healthBar.UpdateHealthBar(CurHp, MaxHp);
    }

    private void OnDisable()
    {
        IsDead = true;
        OnCurHpChanged -= () => _healthBar.UpdateHealthBar(CurHp, MaxHp);
    }

    

    void Update()
    {
        switch (currentState)
        {
            case MonsterState.Idle:
                _animator.SetBool("1_Move", false);
                SearchForEnemies();
                break;

            case MonsterState.Move:
                _animator.SetBool("1_Move", true);
                Move();
                SearchForEnemies();
                break;

            case MonsterState.Detect:
                if (character != null)
                {
                    _animator.SetBool("1_Move", false);
                    StartCoroutine(AttackRoutine());
                }
                break;
        }
    }

    void SearchForEnemies()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, AttackRange, EnemyLayer);
        if (enemy != null)
        {
            character = enemy.transform;
            currentState = MonsterState.Detect;
        }
    }

    void Move()
    {
        // �ܼ� �̵� ���� (��: �÷��̾� �Է�, AI �̵� ����)
        transform.Translate(Vector2.left * 1f * Time.deltaTime);
    }

    IEnumerator AttackRoutine()
    {
        currentState = MonsterState.Attack;

        while (character != null)
        {
            _animator.SetTrigger("2_Attack");

            yield return Util.GetDelay(AttackInterval);

            SearchForEnemies(); // ���� �� �ٽ� �� Ž��
        }

        currentState = MonsterState.Move;
    }

    /* public void Attack()
     {
         character.GetComponent<IDamageable>().TakeDamage(Base.AttackDamage);
     }*/

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange); // ���� ���� ǥ��
    }

    public void TakeDamage(float Damage)
    {
        CurHp -= Damage;
        ShowDamageText(Damage);

        transform.DOShakePosition(0.3f, 0.2f);

        _spriteRenderer.DOColor(Color.white, 0.1f).OnComplete(() => _spriteRenderer.DOColor(_originColor, 0.1f));

        if (CurHp <= 0)
        {
            CurHp = 0;
            Die();
        }
    }

    private void ShowDamageText(float damage)
    {
        _damageText.text = damage.ToString();
        _damageText.alpha = 1f;
        _damageText.rectTransform.anchoredPosition = Vector2.zero;

        Sequence seq = DOTween.Sequence();
        seq.Append(_damageText.rectTransform.DOAnchorPosY(100f, 0.5f)) // anchoredPosition �������� ���� �̵�
           .Join(_damageText.DOFade(0, 0.5f))
           .OnComplete(() =>
           {
               _damageText.alpha = 0;
               _damageText.rectTransform.anchoredPosition = Vector2.zero;
           });
    }

    void Die()
    {
        _animator.Play("DEATH", 0, 0f);

        // DoTween�� ����Ͽ� ���Ͱ� ������� �ִϸ��̼�
        Sequence deathSequence = DOTween.Sequence();

        // ���̵�ƿ�(����ȭ)
        deathSequence.Append(_spriteRenderer.DOFade(0, 1f));

        // ũ�� ���
        deathSequence.Join(transform.DOScale(Vector3.zero, 1f));

        // �ִϸ��̼� �Ϸ� �� ��Ȱ��ȭ �� ���ʱ�ȭ
        deathSequence.OnComplete(() =>
        {
            _pooledObject.CallReturnPool();
            _characterController._monster = null;
            transform.localScale = Vector3.one;
            _spriteRenderer.DOFade(1, 0f);
            _spawner.Spawn();
            InitStatus(CurChapter, CurStage);
        });
    }
}
