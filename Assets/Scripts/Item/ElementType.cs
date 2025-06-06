using System;

namespace Assets.Scripts.Item
{
    public static class ElementTypeEnum
    {
        [Flags] // ��Ʈ �÷��׸� ���� ����ϰ� ���ִ� �Ӽ�
        public enum ElementType
        {
            None = 0,
            Blood = 1 << 0, // 00000001
            Fire = 1 << 1, // 00000010��
            Water = 1 << 2, // 00000100
            Ice = 1 << 3, // 00001000
            Lightning = 1 << 4, // 00010000
            Bomb = 1 << 5, // 00100000
            Dark = 1 << 6, // 01000000
            Poison = 1 << 7  // 10000000
        }
    }
}