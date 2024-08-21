using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 네임스페이스

public class TigerManager : MonoBehaviour
{
    public string nextSceneName; // 이동할 씬의 이름

    // 충돌 이벤트 핸들러 함수
    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("BULLET"))
        { SceneManager.LoadScene(nextSceneName);}
        
    }
}