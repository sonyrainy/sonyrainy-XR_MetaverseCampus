using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip backgroundMusicClip; // 배경음으로 사용할 오디오 클립
    public string nextSceneName; // 전환할 씬의 이름
    private AudioSource audioSource; // 오디오 소스
    public float musicDelay = 30f; // 배경음악 지연 시간 (초)
    public float sceneDelay = 120f; // 씬 전환 지연 시간 (초)

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true; // 배경음이 반복 재생되도록 설정
        audioSource.playOnAwake = false; // 시작 시 자동 재생 비활성화

        // 배경음악 재생 및 씬 전환 시작
        StartCoroutine(ManageBackgroundMusicAndScene());
    }

    IEnumerator ManageBackgroundMusicAndScene()
    {
        // 배경음악 지연 시간 동안 대기
        yield return new WaitForSeconds(musicDelay);

        // 배경음악 재생
        audioSource.Play();

        // 씬 전환 지연 시간 동안 추가 대기 (총 sceneDelay 만큼 대기)
        yield return new WaitForSeconds(sceneDelay - musicDelay);

        // 씬 전환
        SceneManager.LoadScene(nextSceneName);
    }
}