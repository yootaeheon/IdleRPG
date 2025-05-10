using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] float[] layerMoveSpeed;         // z ���� �ٸ� ��� ���̾� �� �̵� �ӵ�

    private Vector2[] layerOffsets;

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

        layerOffsets = new Vector2[backgroundCount];

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
            layerOffsets[i] += Vector2.right * layerMoveSpeed[i] * Time.deltaTime;
            materials[i].SetTextureOffset("_MainTex", layerOffsets[i]);
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
            /*DOTween.To(() => layerMoveSpeed[i], x => layerMoveSpeed[i] = x, stackSpeed, 1f);*/
            stackSpeed += 0.01f;
        }
    }

    public void ResetLayerMoveSpeed()
    {
        for (int i = 1; i < backgroundCount; i++)
        {
            layerMoveSpeed[i] = 0;
            /* int index = i; // Ŭ���� ĸó ������ ���� ����
             DOTween.To(() => layerMoveSpeed[index], x => layerMoveSpeed[index] = x, 0f, 0.2f);*/
        }
    }

}