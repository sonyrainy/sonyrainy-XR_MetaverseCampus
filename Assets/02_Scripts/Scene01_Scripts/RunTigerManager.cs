using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTigerManager : MonoBehaviour
{
     public GameObject targetObject; // 퍼블릭으로 받아오는 게임 오브젝트

    // 트리거 이벤트 핸들러 함수
    private void OnTriggerEnter(Collider other)
    {
        // 트리거된 오브젝트가 플레이어인지 확인 (플레이어 태그를 "Player"로 설정했다고 가정)
        if (other.CompareTag("PLAYER"))
        {
            targetObject.SetActive(true);
        }
    }
}
