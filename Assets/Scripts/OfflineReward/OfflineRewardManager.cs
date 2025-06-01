/*using System;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

// ���� SDK (��: Unity Ads ��� ��)
using UnityEngine.Advertisements;

public class OfflineRewardManager : MonoBehaviour
{
    private DatabaseReference dbRef;              // Firebase Realtime Database ����
    private int goldPerSecond = 10;               // �ʴ� ���� ���
    private readonly long maxRewardSeconds = 21600; // �ִ� ���� ���� �ð�: 6�ð� = 21,600��

    private long calculatedSeconds = 0;           // ���� ��� �ð� (��)
    private int baseReward = 0;                   // �⺻ ����

    void Start()
    {
        // Firebase Database �ʱ�ȭ
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;

        // �α��ε� ������ ��� �������� ���� üũ
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            CheckOfflineReward();
        }
    }

    // ���� ��׶���� ��ȯ�� �� �α׾ƿ� �ð� ����
    void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveLogoutTime();
    }

    // ���� ������ ����� �� �α׾ƿ� �ð� ����
    void OnApplicationQuit()
    {
        SaveLogoutTime();
    }

    /// <summary>
    /// ���� UTC �ð��� ���н� Ÿ�ӽ������� ���� (�α׾ƿ� �ð�)
    /// </summary>
    void SaveLogoutTime()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
        if (string.IsNullOrEmpty(uid)) return;

        long nowUnixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        dbRef.Child("users").Child(uid).Child("lastLogoutTime").SetValueAsync(nowUnixTime);
    }

    /// <summary>
    /// ����� �α׾ƿ� �ð��� ���� �ð��� ���Ͽ� �������� ���� ���
    /// </summary>
    public void CheckOfflineReward()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
        if (string.IsNullOrEmpty(uid)) return;

        long nowUnix = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Firebase���� ������ �α׾ƿ� �ð� ��������
        FirebaseDatabase.DefaultInstance
            .GetReference("users").Child(uid).Child("lastLogoutTime")
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    long lastLogoutUnix = Convert.ToInt64(task.Result.Value);

                    // ���� ��� �ð� ���
                    calculatedSeconds = nowUnix - lastLogoutUnix;

                    // �ִ� ���� �ð�(6�ð�)�� �ʰ��ϸ� �߶�
                    if (calculatedSeconds > maxRewardSeconds)
                        calculatedSeconds = maxRewardSeconds;

                    // ���� ��� (�ʴ� goldPerSecond ���)
                    baseReward = (int)(calculatedSeconds * goldPerSecond);

                    Debug.Log($"[�������� ����] ��� �ð�: {calculatedSeconds}��, �⺻ ����: {baseReward}");

                    // ����ڿ��� ���� ���� ���θ� ���� �˾� ǥ��
                    UIManager.Instance.ShowOfflineRewardPopup(baseReward, OnAdWatchSelected, OnAdDeclined);
                }
                else
                {
                    Debug.Log("[�������� ����] �α׾ƿ� ����� ���ų� �����͸� �ҷ����� ���߽��ϴ�.");
                }
            });
    }

    /// <summary>
    /// ����ڰ� ���� ��û�� ���� �ʰ� �׳� ������ ���� ���
    /// </summary>
    void OnAdDeclined()
    {
        GiveReward(baseReward);
    }

    /// <summary>
    /// ����ڰ� ���� ��û�� �������� ���
    /// </summary>
    void OnAdWatchSelected()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions
            {
                resultCallback = HandleAdResult
            });
        }
        else
        {
            Debug.Log("[����] ���� �غ���� �ʾҽ��ϴ�. �⺻ ������ �����մϴ�.");
            GiveReward(baseReward);
        }
    }

    /// <summary>
    /// ���� ��û ����� ���� ���� ó��
    /// </summary>
    void HandleAdResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("[����] ���� ��û �Ϸ� - 2�� ���� ����");
            GiveReward(baseReward * 2);
        }
        else
        {
            Debug.Log("[����] ���� ��ŵ �Ǵ� ���� - �⺻ ���� ����");
            GiveReward(baseReward);
        }
    }

    /// <summary>
    /// ���� ��� ���� ���� �� UI �˸�
    /// </summary>
    void GiveReward(int rewardAmount)
    {
        GameManager.Instance.AddGold(rewardAmount);                        // ��� ����
        UIManager.Instance.ShowFinalRewardToast(rewardAmount);            // UI�� ���� �˸� ǥ��
        Debug.Log($"[�������� ����] ���� ����: {rewardAmount} ���");
    }

    public void ShowOfflineRewardPopup(int baseReward, Action onAdWatch, Action onDecline)
    {
        // ����:
        // "6�ð� ���� 3600 ��带 �������ϴ�!"
        // [���� ���� 2�� �ޱ�] �� onAdWatch()
        // [�׳� �ޱ�] �� onDecline()
    }

    // ���� ���� ���� �� �佺Ʈ �޽��� �Ǵ� �˾�
    public void ShowFinalRewardToast(int reward)
    {
        // ����: "�� 7200 ��带 ȹ���߽��ϴ�!"
    }
}
*/