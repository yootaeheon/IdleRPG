using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene;

    public Slider loadingBar;

    private void Start()
    {
        StartCoroutine(StartLoadingScene());
    }

    /// <summary>
    /// �ε� ���� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator StartLoadingScene()
    {
        yield return null;

        // nextScene�� ����� �̸��� ���� �񵿱�� �ε�
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false; // �� ��ȯ�� �������� �ϱ� ����

        float timer = 0;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            // �ε� ������
            if (op.progress < 0.9f)
            {
                // ���� ����Ͽ� �ε巴�� �ε� ǥ�� ����
                loadingBar.value = Mathf.Lerp(loadingBar.value, op.progress, timer);

                if (loadingBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            // �ε� �Ϸ� ��
            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1f, timer);

                if (loadingBar.value == 1f)
                {
                    yield return new WaitForSeconds(2f);
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }
}
