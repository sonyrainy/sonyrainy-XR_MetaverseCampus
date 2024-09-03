using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGameManager : MonoBehaviour
{
    public string enemyTag = "ENEMY"; // 적 오브젝트들의 태그
    public string boolParameterName = "IsLookingAround"; // Animator의 Bool 파라미터 이름
    public string WeaponTag = "WEAPON";

    public GameObject Rain;
    public GameObject weatherText;
    
    // 활성화할 게임 오브젝트(fog)
    public GameObject objectToActivate;

    // 5초 뒤에 파괴할 게임 오브젝트
    public GameObject objectToDestroy;

    void Start()
    {
        // 처음에는 특정 오브젝트를 비활성화
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }

        // 5초 뒤에 파괴할 오브젝트 설정
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy, 5f); // 5초 후에 파괴
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 씬에 있는 모든 적 오브젝트 찾기
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            // 각 적 오브젝트의 Animator에 접근하여 bool 파라미터 설정
            foreach (GameObject enemy in enemies)
            {
                Animator enemyAnimator = enemy.GetComponent<Animator>();

                if (enemyAnimator != null)
                {
                    enemyAnimator.SetBool(boolParameterName, true);
                }
            }

            // 비 효과 활성화
            EnableRainEffect();

            // 특정 오브젝트 활성화
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }

            GameObject weapon = GameObject.FindGameObjectWithTag(WeaponTag);
            ShootHong shootHong = weapon.GetComponent<ShootHong>();

            if (shootHong != null)
            {
                shootHong.canShoot = true;
            }
        }
    }

    void EnableRainEffect()
    {
        Rain.SetActive(true);
        weatherText.SetActive(true);

        RenderSettings.skybox.SetColor("_Tint", Color.gray); // 스카이박스 색상을 어둡게 변경
    }
}
