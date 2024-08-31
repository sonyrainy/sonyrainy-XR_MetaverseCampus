using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위해 필요

public class FinalClimb : MonoBehaviour
{
    public string sceneName; // 이동할 씬의 이름

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 PLAYER 태그를 가지고 있는지 확인
        if (other.CompareTag("PLAYER"))
        {
            // 특정 씬으로 이동
            SceneManager.LoadScene(sceneName);
        }
    }
}
