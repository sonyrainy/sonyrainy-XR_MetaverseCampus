using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagement 네임스페이스 추가
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Material newSkybox; // 변경할 Skybox 소재
    public GameObject rainParticleSystem; // 비 효과 Particle System
    public GameObject objectToActivate; // 적이 달아날 때 활성화할 오브젝트
    public string nextSceneName; // 다음 씬의 이름
    private int enemiesKilled = 0; // 죽인 적의 수
    private List<Enemy> allEnemies = new List<Enemy>();

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 시작할 때 모든 Enemy 객체를 찾아 리스트에 추가
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            allEnemies.Add(enemy);
            enemy.enabled = false; // 적들 비활성화
        }

        // 3초 후에 적들 활성화 및 특정 오브젝트 파괴
        StartCoroutine(ActivateEnemiesAndDestroyObject(3f));
    }

    private IEnumerator ActivateEnemiesAndDestroyObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        foreach (var enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.enabled = true; // 적들 활성화
            }
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        allEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        allEnemies.Remove(enemy);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled == 3)
        {
            StartCoroutine(ChangeSkyboxOverTime(new Color(0.5f, 0.5f, 0.5f), 1f)); // 약간 어두운 상태로 변경
        }

        if (enemiesKilled == 10)
        {
            StartRain();
            MakeEnemiesFlee();
        }
    }

    private IEnumerator ChangeSkyboxOverTime(Color targetTint, float targetExposure)
    {
        // Skybox 변경
        RenderSettings.skybox = newSkybox;

        float duration = 5f; // 서서히 변경되는 시간 (초)
        float elapsed = 0f;
        Color initialTint = RenderSettings.skybox.HasProperty("_Tint") ? RenderSettings.skybox.GetColor("_Tint") : Color.white;
        float initialExposure = RenderSettings.skybox.HasProperty("_Exposure") ? RenderSettings.skybox.GetFloat("_Exposure") : 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            if (RenderSettings.skybox.HasProperty("_Tint"))
            {
                RenderSettings.skybox.SetColor("_Tint", Color.Lerp(initialTint, targetTint, elapsed / duration));
            }
            if (RenderSettings.skybox.HasProperty("_Exposure"))
            {
                RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(initialExposure, targetExposure, elapsed / duration));
            }
            yield return null;
        }

        if (RenderSettings.skybox.HasProperty("_Tint"))
        {
            RenderSettings.skybox.SetColor("_Tint", targetTint);
        }
        if (RenderSettings.skybox.HasProperty("_Exposure"))
        {
            RenderSettings.skybox.SetFloat("_Exposure", targetExposure);
        }
    }

    private void StartRain()
    {
        // 비 효과 시작
        if (rainParticleSystem != null)
        {
            rainParticleSystem.SetActive(true);
        }
    }

    private void MakeEnemiesFlee()
    {
        foreach (var enemy in allEnemies)
        {
            if (enemy != null)
            {
                enemy.FleeFromPlayer();
            }
        }

        // 비활성화된 오브젝트를 활성화
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // 5초 후에 게임 종료 대신 씬 변경
        Invoke("ChangeScene", 5f);
    }

    private void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set.");
        }
    }
}
