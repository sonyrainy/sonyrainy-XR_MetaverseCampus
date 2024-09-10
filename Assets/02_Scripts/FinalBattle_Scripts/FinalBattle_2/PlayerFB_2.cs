using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요

public class PlayerFB_2 : MonoBehaviour
{
    public int health = 10; // 플레이어의 초기 체력
    public AudioClip hurtSound; // 플레이어가 맞았을 때 재생될 신음소리
    public AudioSource audioSource; // 오디오 소스

    void Update()
    {
        // 플레이어의 체력이 0 이하가 되면 씬 재시작
        if (health <= 0)
        {
            RestartScene();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // 플레이어의 체력을 감소시킴
        Debug.Log($"Player health: {health}");

        // 신음소리 재생
        if (audioSource != null && hurtSound != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

        // 체력이 0 이하가 되면 씬 재시작
        if (health <= 0)
        {
            RestartScene();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // ENEMY 태그를 가진 적과 충돌 시
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        Debug.Log("Restarting scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 재시작
    }
}
