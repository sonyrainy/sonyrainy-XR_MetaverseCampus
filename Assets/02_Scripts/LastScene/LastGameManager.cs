using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastGameManager : MonoBehaviour
{
    public string enemyTag = "ENEMY"; // 적 오브젝트들의 태그
    public string boolParameterName = "IsLookingAround"; // Animator의 Bool 파라미터 이름
    public Color fogColor = Color.gray; // 안개의 색상
    public float fogDensity = 0.05f; // 안개의 밀도

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
        }
    }

    void EnableRainEffect()
    {
        // 유니티에서 비 효과를 활성화하려면, 일반적으로 파티클 시스템을 사용합니다.
        // 그러나 여기서는 파티클 시스템 없이, 오브젝트나 시각적 효과를 대체할 방법을 구현합니다.
        // 예를 들어, 카메라에 비 맞는 효과를 추가하거나 스카이박스를 어둡게 변경할 수 있습니다.

        // 예시: 스카이박스 변경
        RenderSettings.skybox.SetColor("_Tint", Color.gray); // 스카이박스 색상을 어둡게 변경
    }
}
