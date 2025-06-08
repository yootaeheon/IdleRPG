using Firebase.Database;
using UnityEngine;

public class ProgressSaver : MonoBehaviour
{
    public ProgressSO progressSO;

    public void SaveProgressToFirebase()
    {
        string uid = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser?.UserId;
        if (string.IsNullOrEmpty(uid)) return;

        ProgressDataDTO data = new ProgressDataDTO(
            progressSO.Chapter,
            progressSO.Stage,
            progressSO.KillCount
        );

        string json = JsonUtility.ToJson(data);
        FirebaseDatabase.DefaultInstance
            .RootReference
            .Child("users")
            .Child(uid)
            .Child("_progressData")
            .SetRawJsonValueAsync(json)
            .ContinueWith(task =>
            {
                if (task.IsCompleted)
                    Debug.Log("���൵ ���� �Ϸ�!");
                else
                    Debug.LogWarning("���൵ ���� ����: " + task.Exception);
            });
    }
}
