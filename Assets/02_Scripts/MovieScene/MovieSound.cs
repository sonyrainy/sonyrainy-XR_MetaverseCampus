using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class BackgroundMusicManager : MonoBehaviour
{
    public string nextSceneName; // 전환할 씬의 이름

    void Start()
    {
        StartCoroutine(TransitionAfterDelay(90f)); // 70초 후에 씬 전환 시작
    }

    IEnumerator TransitionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 70초 대기
        SceneManager.LoadScene(nextSceneName); // 지정한 씬으로 전환
    }
}