using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float distance = 10f;
    RaycastHit2D hit; //  2D ���� RaycastHit

    private void Update()
    {
        // 2D�� Raycast ���
        hit = Physics2D.Raycast(transform.position, transform.right, distance);

        // ����׿� �� (Ray�� ������ �¾Ҵ��� Ȯ��)
        Debug.DrawRay(transform.position, transform.right * distance, Color.red);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            Debug.Log(hit.collider.name);
        }

        if (Input.GetKeyDown(KeyCode.Space) && hit.collider != null)
        {
            // ��ƼŬ ��� ��ġ�� �浹�� ��ġ ����
            /*EffectManager.Instance.PlayEffect(, hit.point, hit.collider.transform);*/
        }
    }
}
