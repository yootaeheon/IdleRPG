using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� ��� ����Ʈ �Ŵ���
/// ������ ���� ���� �ִ� ����Ʈ�� ��û�� ����Ʈ�� �ٸ��� ������ ���� ���� ����
/// �̱������� �۵��� (�ڷ�ƾ���� ���Ͻ�Ű������ Static�� ����� �� ����(MonoBehaviour�� ��ӹ޾ƾ� ��))
/// </summary>
public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    [SerializeField] EffectSO _effectData;
    public EffectSO EffectData { get { return _effectData; } set { _effectData = value; } }

    // �ϳ��� ���� ���ø� ���, �ִ� 2������ ����
    private Stack<GameObject> effectPool = new();

    private void Awake()
    {
        SetSingleton();
    }

    public void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(GameObject effectPrefab, Vector3 position, Transform followTransform = null, Quaternion rotation = default)
    {
        if (rotation == default)
        {
            rotation = Quaternion.identity;
        }

        if (effectPrefab == null)
        {
            Debug.LogWarning("[EffectManager] Effect Prefab is null.");
            return;
        }

        GameObject effectInstance = null;

        // ���ÿ� ������Ʈ�� �ְ� �̸��� ��ġ�ϸ� ������
        if (effectPool.Count > 0 && effectPool.Peek().name == effectPrefab.name)
        {
            effectInstance = effectPool.Pop();
        }
        // �̸��� �ٸ��� Ǯ ���� ���� ����
        else
        {
            // ���� ������Ʈ�� ����
            while (effectPool.Count > 0)
            {
                var pooledEffect = effectPool.Pop();
                if (pooledEffect != null)
                {
                    Destroy(pooledEffect);
                }
            }

            effectInstance = Instantiate(effectPrefab);
            effectInstance.name = effectPrefab.name;
        }

        // ��ġ, ȸ��, �θ� ���� �� Ȱ��ȭ
        effectInstance.transform.SetPositionAndRotation(position, rotation);
        effectInstance.transform.SetParent(followTransform);
        SetActiveRecursively(effectInstance, true);

        float maxLifeTime = CalculateMaxLifetime(effectInstance);
        StartCoroutine(ReturnToPool(effectInstance, maxLifeTime));
    }

    private float CalculateMaxLifetime(GameObject effectObj)
    {
        float maxLifeTime = 0f;

        foreach (ParticleSystem ps in effectObj.GetComponentsInChildren<ParticleSystem>(true))
        {
            var main = ps.main;
            main.playOnAwake = true;
            main.stopAction = ParticleSystemStopAction.Disable;

            float startLifetime = 0f;

            if (main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants)
            {
                startLifetime = Mathf.Max(main.startLifetime.constantMin, main.startLifetime.constantMax);
            }
            else
            {
                startLifetime = main.startLifetime.constant;
            }

            maxLifeTime = Mathf.Max(maxLifeTime, main.duration + startLifetime);
        }

        return maxLifeTime;
    }

    private IEnumerator ReturnToPool(GameObject effectObj, float delay)
    {
        yield return Util.GetDelay(delay);

        if (!effectObj) yield break;

        effectObj.SetActive(false);
        effectObj.transform.SetParent(transform);

        if (effectPool.Count < 2)
        {
            effectPool.Push(effectObj);
        }
        else
        {
            Destroy(effectObj);
        }
    }

    // Ǯ���� ��ƼŬ�� �ڽ� ������Ʈ �� �Ϻΰ� ��Ȱ��ȭ�� ä�� ���� �־,
    // �ٽ� Ȱ��ȭ�Ǿ��� �� ��ü ����Ʈ�� ����� ������ �ʴ� ������ �����ϴ� �ڵ�
    private void SetActiveRecursively(GameObject obj, bool isActive)
    {
        obj.SetActive(isActive);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, isActive);
        }
    }
}
