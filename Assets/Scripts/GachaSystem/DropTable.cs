using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ������ ��� ���̺� Ŭ����
/// �� ����(1~10)�� ���� ��� Ȯ���� �����ϰ� ����մϴ�.
/// </summary>
[System.Serializable]
public class DropTable
{
    public string name; // ��� ���̺� �̸� (���� �̸�)
    public float[] weights = new float[10]; // �� ����(1~10)�� Ȯ�� ����ġ

    public DropTable(string name)
    {
        this.name = name;
    }

    /// <summary>
    /// ���Ժ��� ��� Ȯ�� ���� ����
    /// �÷��̾� ������ ǥ�������� ������� ����ġ ���̺��� ����
    /// </summary>
    /// <param name="characterLevel">���� ĳ���� ���� (1~100)</param>
    /// <param name="stdDev">ǥ������: �������� �л��� ŭ</param>
    public void GenerateDistribution(int characterLevel, float stdDev)
    {
        // ��� ���� ���: ĳ���� ������ �������� ��յ� ��� (�ִ� 10)
        float mean = Mathf.Lerp(2f, 10f, characterLevel / 100f);

        float total = 0f;

        // ���� 1~10�� ���� ���Ժ��� Ȯ�� ���
        for (int i = 0; i < 10; i++)
        {
            weights[i] = Gaussian(i + 1, mean, stdDev); // i + 1 == ������ ����
            total += weights[i]; // ��ü �ջ� (����ȭ��)
        }

        // ��ü ������ ����ȭ �� ������ 1�� �ǵ��� Ȯ��ȭ
        for (int i = 0; i < 10; i++)
        {
            weights[i] /= total;
        }
    }

    /// <summary>
    /// ���� ����ġ ������� ������ ���� ����
    /// Ȯ�� ���� ��� ���
    /// </summary>
    /// <returns>���õ� ������ ���� (1~10)</returns>
    public int GetWeightedItemLevel()
    {
        float rand = UnityEngine.Random.value; // 0~1 ���� ����
        float cumulative = 0f; // ���� Ȯ��

        for (int i = 0; i < 10; i++)
        {
            cumulative += weights[i];
            if (rand <= cumulative)
                return i + 1; // ���� ������ �ε��� + 1
        }

        return 1; // ���� ������ Fallback
    }

    /// <summary>
    /// ���Ժ���(���콺) Ȯ�� �е� �Լ�
    /// </summary>
    /// <param name="x">���� ���� (������ ����)</param>
    /// <param name="mean">��� (�߽ɰ�)</param>
    /// <param name="stdDev">ǥ������</param>
    /// <returns>�ش� ������ Ȯ�� ��</returns>
    float Gaussian(float x, float mean, float stdDev)
    {
        float a = 1.0f / (stdDev * Mathf.Sqrt(2.0f * Mathf.PI)); // ����ȭ ���
        float b = Mathf.Exp(-Mathf.Pow(x - mean, 2) / (2.0f * stdDev * stdDev)); // �����Լ�
        return a * b; // ���콺 �Լ� ���
    }
}
