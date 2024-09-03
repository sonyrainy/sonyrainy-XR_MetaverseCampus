using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip initialBackgroundMusic; // 초기 배경음악 클립

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        PlayInitialBackgroundMusic();
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
