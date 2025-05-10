using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ Ǯ; �������� �̸� �����س��� Ȱ��ȭ/��Ȱ��ȭ ������� ����;
/// �޸� �ܺ� ����ȭ ����;
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<PooledObject> _pool = new List<PooledObject>();

    [SerializeField] PooledObject _prefab;

    [SerializeField] int _size;

    private void Awake()
    {
        for (int i = 0; i < _size; i++)
        {
            PooledObject instance = Instantiate(_prefab);
            instance.gameObject.SetActive(false);
            instance._returnPool = this;
            instance.transform.parent = transform;
            _pool.Add(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (_pool.Count > 0)
        {
            PooledObject instance = _pool[_pool.Count - 1];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = null;
            instance.gameObject.SetActive(true);

            _pool.RemoveAt(_pool.Count - 1);

            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(_prefab, position, rotation);
            instance._returnPool = this;
            _pool.Add(instance);
            return instance;
        }
    }

    public void RetrunPool(PooledObject instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;
        _pool.Add(instance);
    }
}
