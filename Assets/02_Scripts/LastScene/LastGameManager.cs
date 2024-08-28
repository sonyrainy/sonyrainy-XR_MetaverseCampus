using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGameManager : MonoBehaviour
{
    public string enemyTag = "ENEMY"; // 적 오브젝트들의 태그
    public string boolParameterName = "IsLookingAround"; // Animator의 Bool 파라미터 이름
     public string WeaponTag = "WEAPON";
    public Color fogColor = Color.gray; // 안개의 색상
    public float fogDensity = 0.05f; // 안개의 밀도

    public GameObject Rain;
    public GameObject weatherText;

    void Start()
    {
        // 처음에는 안개를 비활성화
        RenderSettings.fog = false;
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
                
                enemyAnimator.SetBool(boolParameterName, true);
                
            }

            // 비 효과 활성화
            EnableRainEffect();

            // 안개 효과 활성화
            RenderSettings.fog = true;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;

            GameObject weapon = GameObject.FindGameObjectWithTag(WeaponTag);
            
            ShootHong shootHong = weapon.GetComponent<ShootHong>();
                
            shootHong.canShoot = true;
                
            
            
        }
    }

    void EnableRainEffect()
    {
        Rain.SetActive(true);
        weatherText.SetActive(true);

        RenderSettings.skybox.SetColor("_Tint", Color.gray); // 스카이박스 색상을 어둡게 변경
    }
}
