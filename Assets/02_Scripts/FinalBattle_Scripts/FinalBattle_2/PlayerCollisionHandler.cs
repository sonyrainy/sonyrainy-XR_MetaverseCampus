using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            RestartScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ENEMY"))
        {
            RestartScene();
        }
    }

    private void RestartScene()
    {
        // 현재 활성화된 씬을 다시 로드합니다.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
