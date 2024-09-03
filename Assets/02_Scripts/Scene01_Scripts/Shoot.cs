using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private float ImpulseForce = 20;
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private AudioClip shootSound; // 총 사운드 클립
    [SerializeField]
    private MeshRenderer muzzleFlash; // Muzzle Flash Mesh Renderer
    [SerializeField]
    private float flashDuration = 0.1f; // Muzzle Flash 지속 시간
    [SerializeField]
    private float flashScaleMin = 0.8f; // Muzzle Flash 최소 크기
    [SerializeField]
    private float flashScaleMax = 2.0f; // Muzzle Flash 최대 크기

    private AudioSource audioSource; // AudioSource 변수
    private bool shoot = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가 및 초기화
        audioSource.clip = shootSound; // 사운드 클립 설정

        if (muzzleFlash != null)
        {
            muzzleFlash.enabled = false; // 초기에는 Muzzle Flash를 비활성화
        }
    }

    void Update()
    {
        if (shoot)
        {
            FireBullet();
            StartCoroutine(ShowMuzzleFlash()); // Muzzle Flash를 표시하는 코루틴 실행
            shoot = false;
        }
    }

    public void Fire()
    {
        shoot = true;
        audioSource.Play(); // 사운드 재생
    }

    private void FireBullet()
    {
        GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);
        Rigidbody rb = go.GetComponent<Rigidbody>();
        rb.AddForce(ImpulseForce * muzzle.forward, ForceMode.Impulse);
        Destroy(go, 2);
    }

    private IEnumerator ShowMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            // Muzzle Flash 크기 랜덤 설정
            float scale = Random.Range(flashScaleMin, flashScaleMax);
            muzzleFlash.transform.localScale = Vector3.one * scale;

            // Muzzle Flash를 활성화
            muzzleFlash.enabled = true;

            // 지정된 시간 동안 Muzzle Flash 표시
            yield return new WaitForSeconds(flashDuration);

            // Muzzle Flash를 비활성화
            muzzleFlash.enabled = false;
        }
    }
}
