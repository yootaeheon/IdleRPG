using UnityEngine;

public class ParallaxBackground_02 : MonoBehaviour
{
    [SerializeField] float[] layerMoveSpeed;         // z ���� �ٸ� ��� ���̾� �� �̵� �ӵ�

    private int backgroundCount;                     // Layer ��

    private Material[] materials;                    // ��� ��ũ���� ���� Material �迭 ����

    private void Awake()
    {
        // ����� ������ ���ϰ�, ��� ������ ������ GameObject �迭 ����
        backgroundCount = transform.childCount;
        GameObject[] backgrounds = new GameObject[backgroundCount];

        // �� ����� material�� �̵� �ӵ��� ������ �迭 ����
        materials = new Material[backgroundCount];
        layerMoveSpeed = new float[backgroundCount];


        for (int i = 0; i < backgroundCount; ++i)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            materials[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        SetLayerMoveSpeed();
    }

    /// <summary>
    /// �� Layer�� moveSpeed ����
    /// </summary>
    private void SetLayerMoveSpeed()
    {
        float stackSpeed = 0.01f;
        for (int i = 1; i < backgroundCount; i++)
        {
            layerMoveSpeed[i] = stackSpeed;
            stackSpeed += 0.01f;
        }
    }

    /// <summary>
    /// �� Layer ��ũ��
    /// </summary>
    private void Update()
    {
        for (int i = 1; i < materials.Length; ++i)
        {
            materials[i].SetTextureOffset("_MainTex", Vector2.right * layerMoveSpeed[i] * Time.time);
        }
    }
}