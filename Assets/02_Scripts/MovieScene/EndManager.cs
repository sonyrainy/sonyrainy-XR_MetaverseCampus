using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가
using System.Collections;

public class EndManager : MonoBehaviour
{
    public float countdownTime = 120f; // 120초 타이머

    private void OnEnable()
    {
        // 씬 로드 이벤트에 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트에서 메서드 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되면 타이머 코루틴 시작
        StartCoroutine(EndGameAfterTime(countdownTime));
    }

    private IEnumerator EndGameAfterTime(float time)
    {
        // 지정된 시간 동안 대기
        yield return new WaitForSeconds(time);

        EndGame();
    }

    private void EndGame()
    {
#if UNITY_EDITOR
        // 에디터에서 실행 중인 경우 에디터 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서 실행 중인 경우 애플리케이션 종료
        Application.Quit();
#endif
    }
}
