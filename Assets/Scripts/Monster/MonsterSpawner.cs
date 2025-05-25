using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instantiate ��� GetPool�� ���� �뿩�ؿ�
/// </summary>
public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool _pool;

    [SerializeField] Transform _spawnPoint;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        PooledObject instance = _pool.GetPool(_spawnPoint.position, _spawnPoint.rotation);
        ChapterManager.Instance.ProgressInfo.KillCount--;
    }
}
