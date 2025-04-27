using UnityEngine;

public class ParallaxBackground_X : MonoBehaviour
{
    [SerializeField] Transform _target;            // ���� ���� �̾����� ���

    [SerializeField] float _scrollAmount;          // �̾����� �� ��� ���� �Ÿ�

    [SerializeField] float _moveSpeed;             // �̵� �ӵ�

    [SerializeField] Vector3 _moveDirection;       // �̵� ����

    private void Update()
    {
        // ����� _moveDirection �������� _moveSpeed �ӵ��� �̵�
        transform.position += _moveDirection * _moveSpeed * Time.deltaTime;

        // ����� ������ ������ ����� ��ġ �缳��
        if (transform.position.x <= -_scrollAmount)
        {
            // �̵� ������ �ݴ� ���� * _scrollAmount��ŭ �������� ��ġ �缳��
            transform.position = _target.position - _moveDirection * _scrollAmount;
        }
    }

}
