using UnityEngine;

public class Bullet_Player : MonoBehaviour
{
    public int damage = 1; // 총알의 피해량

    void Start()
    {
        // 10초 후에 총알 제거
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ENEMY"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        // 어떤 오브젝트와 충돌하든 총알 제거
        Destroy(gameObject);
    }
}
