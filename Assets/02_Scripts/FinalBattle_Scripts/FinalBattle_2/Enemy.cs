using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    private float minMoveSpeed = 1.3f;
    private float maxMoveSpeed = 2.8f;
    public int health = 3;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDead = false;
    private bool isFleeing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.speed = Random.Range(minMoveSpeed, maxMoveSpeed);

        navMeshAgent.enabled = false;
        animator.SetBool("isRunning", false);

        StartCoroutine(WaitAndStartChasing(3f));
    }

    private Vector3 lastPlayerPosition;

    void Update()
    {
        if (isDead || isFleeing || !navMeshAgent.enabled) return;

        if (health <= 0)
        {
            navMeshAgent.isStopped = true;
        }
        else if (navMeshAgent.isOnNavMesh && player.position != lastPlayerPosition)
        {
            navMeshAgent.SetDestination(player.position);
            lastPlayerPosition = player.position;
        }
    }

    private IEnumerator WaitAndStartChasing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!isDead)
        {
            navMeshAgent.enabled = true;
            if (navMeshAgent.isOnNavMesh)
            {
                animator.SetBool("isRunning", true);
                navMeshAgent.SetDestination(player.position);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} died.");
        animator.SetBool("isRunning", false);
        animator.SetTrigger("isDead");
        navMeshAgent.isStopped = true;
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        GetComponent<Collider>().enabled = false;

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(10f);

        Debug.Log($"{gameObject.name} is destroyed.");
        Destroy(gameObject);
    }

    public void FleeFromPlayer()
    {
        if (isDead || !navMeshAgent.isOnNavMesh) return;

        isFleeing = true;
        Debug.Log($"{gameObject.name} is fleeing from player.");
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * 10f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleePosition, out hit, 10f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position - fleeDirection * 10f);
        }

        navMeshAgent.speed = maxMoveSpeed;
        navMeshAgent.isStopped = false;
        animator.SetBool("isRunning", true);
    }

    // 추가된 코드: 총알에 맞았을 때 처리
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1); // 총알의 데미지를 1로 가정
            Destroy(other.gameObject); // 총알 파괴
        }
    }
}
