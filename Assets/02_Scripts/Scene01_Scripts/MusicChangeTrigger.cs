using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    public AudioClip newBackgroundMusic; // 새 배경음으로 사용할 오디오 클립

    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어인지 확인 (플레이어 태그를 "Player"로 설정했다고 가정)
        if (other.CompareTag("Player"))
        {
            // BackgroundMusicPlayer 오브젝트 찾기
            BackgroundMusicPlayer musicPlayer = FindObjectOfType<BackgroundMusicPlayer>();

            if (musicPlayer != null)
            {
                // 배경음 변경
                musicPlayer.ChangeBackgroundMusic(newBackgroundMusic);

                // 10초 후에 배경음 멈춤
                Invoke("StopBackgroundMusic", 40f);
            }
            else
            {
                Debug.LogWarning("BackgroundMusicPlayer not found in the scene.");
            }
        }
    }

    void StopBackgroundMusic()
    {
        BackgroundMusicPlayer musicPlayer = FindObjectOfType<BackgroundMusicPlayer>();
        if (musicPlayer != null)
        {
            musicPlayer.StopBackgroundMusic();
        }
    }
}

