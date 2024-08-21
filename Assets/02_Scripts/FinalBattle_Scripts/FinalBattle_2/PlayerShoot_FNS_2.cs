using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot_FNS_2 : MonoBehaviour
{
    [SerializeField]
    private float ImpulseForce = 20;
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private AudioClip gunshotClip; // 총소리 오디오 클립

    private bool shoot = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // 3D 사운드
        audioSource.clip = gunshotClip;
    }

    void Update()
    {
        if (shoot)
        {
            Debug.Log("Shooting...");

            GameObject go = Instantiate(bullet, muzzle.position, muzzle.rotation);
            Rigidbody rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(ImpulseForce * muzzle.forward, ForceMode.Impulse);
            }

            // 총소리 재생
            if (audioSource != null)
            {
                audioSource.Play();
            }

            Destroy(go, 2);
            shoot = false;
        }
    }

    public void Fire()
    {
        Debug.Log("Fire method called.");
        shoot = true;
    }
}
