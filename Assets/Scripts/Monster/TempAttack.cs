using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

/// <summary>
/// MonsterController�� �ֻ����� �־���ϴµ� Animator�� �ڽĿ� �־����
/// Attack �ִϸ��̼� �̺�Ʈ�� ȣ���ؾ��ϴµ� ��ũ��Ʈ ��ġ�� Animator ��ġ�� �޶� �����Ͽ� ȣ���� �� �ֵ��� �ӽù��� ��ġ��
/// ���� �ٺ����� ������ StateBehaviour ����غ���
/// </summary>
public class TempAttack : MonoBehaviour
{
    [SerializeField] MonsterController MonsterController;

    public void Attack()
    {
        MonsterController.character.GetComponent<IDamageable>().TakeDamage(MonsterController.Base.AttackDamage);
    }
}
