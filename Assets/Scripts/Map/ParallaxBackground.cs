using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{


    [SerializeField] float[] layerMoveSpeed;         // z ���� �ٸ� ��� ���̾� �� �̵� �ӵ�

    [SerializeField] CharacterController _characterController;

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

    private void OnEnable()
    {
        _characterController.OnEncounterMonster += ResetLayerMoveSpeed;
        _characterController.OnKillMonster += SetLayerMoveSpeed;
    }

    private void OnDisable()
    {
        _characterController.OnEncounterMonster -= ResetLayerMoveSpeed;
        _characterController.OnKillMonster -= SetLayerMoveSpeed;

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

    /// <summary>
    /// �� Layer�� moveSpeed ����
    /// </summary>
    public void SetLayerMoveSpeed()
    {
        float stackSpeed = 0.01f;
        for (int i = 1; i < backgroundCount; i++)
        {
            layerMoveSpeed[i] = stackSpeed;
            stackSpeed += 0.01f;
        }
    }

    public void ResetLayerMoveSpeed()
    {
        for (int i = 1; i < backgroundCount; i++)
        {
            layerMoveSpeed[i] = 0;
        }
    }

}