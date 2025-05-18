using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public enum EquipmentType
    {
        Armor,
        Helmet,
        Back,
        Weapon
    }

    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "EquipItemSO")]
    public class EquipItemSO : ItemSO
    {
        [Header("-------------------------------------------")]
        [SerializeField] EquipmentType _equipmetnType;
        public EquipmentType EquipmentType { get { return _equipmetnType;  } set { _equipmetnType = value; } }

        [Header("Weapon")]
        [SerializeField] int _attackPower;
        public int AttackPower { get { return _attackPower; } set { _attackPower = value; } }

        [SerializeField] int _attackSpeed;
        public int AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }

        [SerializeField] int _criticalChance; // ũ��Ƽ�� Ȯ��
        public int CriticalChance { get { return _criticalChance; } set { _criticalChance = value; } }

        [Header("Armor & Helmet & Back")]
        [SerializeField] int _plusHp; // �߰� Hp
        public int PlusHp { get { return _plusHp; } set { _plusHp = value; } }

        [SerializeField] int _recoverHpPerSecond; // �ʴ� Hp ȸ����
        public int RecoverHpPerSecond { get { return _recoverHpPerSecond; } set { _recoverHpPerSecond = value; } }


    }
}