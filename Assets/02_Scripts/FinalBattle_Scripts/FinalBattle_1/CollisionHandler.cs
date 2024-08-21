using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public string nextSceneName; // 전환할 씬 이름

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("GUN"))
        {
            // Ammo_Pack 오브젝트를 파괴
            Destroy(gameObject);

            // 씬 전환
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
