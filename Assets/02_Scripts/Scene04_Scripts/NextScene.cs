using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string nextSceneName;  // 다음 씬의 이름

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그가 "Player"인지 확인
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // 다음 씬 로드
        SceneManager.LoadScene(nextSceneName);
    }
}
