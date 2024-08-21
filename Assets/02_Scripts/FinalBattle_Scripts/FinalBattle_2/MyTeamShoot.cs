using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTeamShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform bulletSpawnPoint; // 총알이 발사될 위치
    public float fireRate = 1f; // 발사 간격 (초)
    public float bulletSpeed = 20f; // 총알 속도
    public float detectionRange = 30f; // 적을 감지하는 범위
    public AudioClip gunshotClip; // 총소리 오디오 클립

    private Transform targetEnemy;
    private AudioSource audioSource;
    private SFXController_FNS_1 sfxController;

    void Start()
    {
        // SFXController 컴포넌트 가져오기
        sfxController = GetComponent<SFXController_FNS_1>();

        // AudioSource 컴포넌트 추가 및 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // 3D 사운드
        audioSource.clip = gunshotClip;

        // 일정 시간마다 총알 발사
        InvokeRepeating(nameof(FireAtEnemy), 0f, fireRate);
    }

    void Update()
    {
        FindClosestEnemy();
    }

    void FindClosestEnemy()
    {
        float closestDistance = detectionRange;
        targetEnemy = null;

        // "Enemy" 태그를 가진 모든 오브젝트 찾기
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("ENEMY"))
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }
    }

    void FireAtEnemy()
    {
        if (targetEnemy != null)
        {
            // 총알 프리팹 생성
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 총알을 적을 향해 발사
                Vector3 direction = (targetEnemy.position - bulletSpawnPoint.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }

            // 총소리 재생
            if (sfxController != null)
            {
                sfxController.PlaySFX("shoot");
            }
            else
            {
                // SFXController가 없을 경우 AudioSource 사용
                audioSource.Play();
            }
        }
    }
}
