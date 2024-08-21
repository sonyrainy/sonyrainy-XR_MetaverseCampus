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

    private AudioSource audioSource; // AudioSource 변수
    private bool shoot = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트 추가 및 초기화
        audioSource.clip = shootSound; // 사운드 클립 설정
    }

    void Update()
    {
        if (shoot)
        {
            GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            rb.AddForce(ImpulseForce * muzzle.forward, ForceMode.Impulse);
            Destroy(go, 2);
            shoot = false;
        }
    }

    public void Fire()
    {
        shoot = true;
        audioSource.Play(); // 사운드 재생
    }
}