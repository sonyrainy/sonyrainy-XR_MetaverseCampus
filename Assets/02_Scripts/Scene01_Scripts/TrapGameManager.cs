using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrapGameManager : MonoBehaviour
{
    public GameObject uiElement;  // 퍼블릭으로 받아오는 UI 요소
    public List<GameObject> spotLights; // 퍼블릭으로 받아오는 SpotLight 목록
    public List<GameObject> GreenLigts;

    public void OnSelectEntered() // 이벤트 핸들러 함수
    {
        uiElement.SetActive(true);
        
        // 모든 SpotLight 끄기
        foreach (var light in spotLights)
        {
            light.SetActive(false);
        }

        foreach (var light in GreenLigts)
        {
            light.SetActive(true);
        }
    }
}


