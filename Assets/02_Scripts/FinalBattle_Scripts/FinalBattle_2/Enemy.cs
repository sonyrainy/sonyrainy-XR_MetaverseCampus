using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float minMoveSpeed = 3.5f; // 최소 달리기 속도 (NavMeshAgent의 speed 값)
    public float maxMoveSpeed = 5.5f; // 최대 달리기 속도 (NavMeshAgent의 speed 값)
    public int health = 3; // 체력

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDead = false;
    private bool isFleeing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 무작위 달리기 속도 설정
        navMeshAgent.speed = Random.Range(minMoveSpeed, maxMoveSpeed);

        // 시작할 때 NavMeshAgent 비활성화 및 isRunning 애니메이션 false
        navMeshAgent.enabled = false;
        animator.SetBool("isRunning", false);

        // 3초 후에 NavMeshAgent 활성화 및 추적 시작
        StartCoroutine(WaitAndStartChasing(3f));

        // GameManager에 자신 등록
        GameManager.Instance.RegisterEnemy(this);
    }

    void Update()
    {
        if (isDead || isFleeing || !navMeshAgent.enabled) return;

        // 적군이 죽으면 정지
        if (health <= 0)
        {
            navMeshAgent.isStopped = true;
        }
        else if (navMeshAgent.isOnNavMesh)
        {
            // 적이 플레이어를 계속 추적하도록 설정
            navMeshAgent.SetDestination(player.position);
        }
    }

    private IEnumerator WaitAndStartChasing(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!isDead)
        {
            // NavMeshAgent 활성화 및 추적 시작
            navMeshAgent.enabled = true;
            if (navMeshAgent.isOnNavMesh)
            {
                animator.SetBool("isRunning", true); // 달리는 애니메이션 활성화
                navMeshAgent.SetDestination(player.position);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // 이미 죽은 경우 처리하지 않음

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 적군 죽음 처리
        isDead = true;
        Debug.Log($"{gameObject.name} died."); // 적군이 죽었을 때 디버그 로그 출력
        animator.SetBool("isRunning", false); // 달리는 애니메이션 비활성화
        animator.SetTrigger("isDead"); // 죽는 애니메이션 활성화
        navMeshAgent.isStopped = true; // 이동 정지
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        GetComponent<Collider>().enabled = false; // 콜라이더 비활성화

        // GameManager에 자신 제거 알림
        GameManager.Instance.UnregisterEnemy(this);

        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // 죽는 애니메이션이 끝날 때까지 대기
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        // 추가적으로 10초 동안 대기
        yield return new WaitForSeconds(10f);

        // 오브젝트 파괴
        Debug.Log($"{gameObject.name} is destroyed."); // 오브젝트가 파괴될 때 디버그 로그 출력
        Destroy(gameObject);
        GameManager.Instance.EnemyKilled(); // GameManager에 적의 죽음을 알림
    }

    public void FleeFromPlayer()
    {
        if (isDead || !navMeshAgent.isOnNavMesh) return; // 이미 죽은 경우 또는 NavMesh에 없는 경우 처리하지 않음

        isFleeing = true;
        Debug.Log($"{gameObject.name} is fleeing from player."); // 적군이 도망갈 때 디버그 로그 출력
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        Vector3 fleePosition = transform.position + fleeDirection * 10f; // 10 단위 거리만큼 플레이어 반대 방향으로 이동

        // NavMesh 상의 도망가는 위치 설정
        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleePosition, out hit, 10f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position - fleeDirection * 10f);
        }

        navMeshAgent.speed = maxMoveSpeed; // 도망갈 때 최대 속도로 설정
        navMeshAgent.isStopped = false; // NavMeshAgent 다시 시작
        animator.SetBool("isRunning", true); // 달리는 애니메이션 활성화
    }
}
