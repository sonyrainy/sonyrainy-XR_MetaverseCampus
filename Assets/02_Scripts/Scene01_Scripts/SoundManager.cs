using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private static BackgroundMusicPlayer instance;
    private AudioSource audioSource;

    public AudioClip initialBackgroundMusic; // 초기 배경음악 클립

    void Awake()
    {
        // 싱글톤 패턴을 사용하여 오브젝트 중복 생성 방지
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
            PlayInitialBackgroundMusic();
        }
    }

    void PlayInitialBackgroundMusic()
    {
        if (initialBackgroundMusic != null)
        {
            audioSource.clip = initialBackgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void ChangeBackgroundMusic(AudioClip newMusic)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = newMusic;
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}