using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    public Transform player;
    private float minMoveSpeed = 1.0f;
    private float maxMoveSpeed = 2.5f;
    public int health = 3;

    public GameObject bulletPrefab;
    public Transform bulletFirePoint;
    public MeshRenderer muzzleFlash;
    public AudioClip shootSound;
    public AudioSource audioSource;
    private float minShootingInterval = 2f;
    private float maxShootingInterval = 10f; 
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isDead = false;
    private bool isShooting = false;
    private float shootingInterval;

    private float flashDuration;
    private float flashScaleMin;
    private float flashScaleMax;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.speed = Random.Range(minMoveSpeed, maxMoveSpeed);
        shootingInterval = Random.Range(minShootingInterval, maxShootingInterval);

        navMeshAgent.enabled = true;
        animator.SetBool("isRunning", true);

        InitializeMuzzleFlashProperties();

        StartCoroutine(ShootAtIntervals());
    }

    void Update()
    {
        if (isDead || !navMeshAgent.enabled) return;

        if (health <= 0)
        {
            navMeshAgent.isStopped = true;
            return;
        }

        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(player.position);
            animator.SetBool("isRunning", true);
        }
    }

    private void InitializeMuzzleFlashProperties()
    {
        flashDuration = Random.Range(0.1f, 0.3f);
        flashScaleMin = Random.Range(0.8f, 1.5f);
        flashScaleMax = Random.Range(2.0f, 3.0f);
    }

    private IEnumerator ShootAtIntervals()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(shootingInterval);

            if (!isShooting && navMeshAgent.isOnNavMesh)
            {
                StartCoroutine(Shoot());
            }

            shootingInterval = Random.Range(minShootingInterval, maxShootingInterval);
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        animator.SetBool("isShooting", true);

        float originalSpeed = navMeshAgent.speed;
        navMeshAgent.speed = originalSpeed * 0.8f;

        yield return new WaitForSeconds(1.0f);

        FireBullet();
        StartCoroutine(ShowMuzzleFlash());

        yield return new WaitForSeconds(0.5f);

        float recoveryTime = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < recoveryTime)
        {
            navMeshAgent.speed = Mathf.Lerp(navMeshAgent.speed, originalSpeed, elapsedTime / recoveryTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        navMeshAgent.speed = originalSpeed;
        animator.SetBool("isShooting", false);
        isShooting = false;
    }

    private void FireBullet()
    {
        if (bulletPrefab != null && bulletFirePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                Vector3 direction = (player.position - bulletFirePoint.position).normalized;
                bulletRigidbody.velocity = direction * 20f;
            }
        }
    }

    private IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        float scale = Random.Range(flashScaleMin, flashScaleMax);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(flashDuration);
        muzzleFlash.enabled = false;
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

        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void FleeFromPlayer()
    {
        if (isDead || !navMeshAgent.isOnNavMesh) return;

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
        animator.SetBool("isRunning", true);
    }
}
