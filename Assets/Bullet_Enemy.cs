using UnityEngine;

public class Bullet_Enemy : MonoBehaviour
{
    public int damage = 1; // 총알 뎀지

    void Start()
    {
        // 10초 후에 총알 제거
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLAYER"))
        {
            PlayerFB_2 player = other.GetComponent<PlayerFB_2>();
            if (player != null)
            {
                // 플레이어에게 데미지 줌
                player.TakeDamage(damage);

                // 플레이어가 총알에 맞는거
                Debug.Log("Player의 남은 체력: " + player.health);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
