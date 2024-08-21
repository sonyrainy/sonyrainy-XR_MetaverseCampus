using System.Collections;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    public AudioClip soundClip;  // 재생할 사운드 클립
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundClip;

        // 코루틴 시작
        StartCoroutine(PlaySoundRoutine());
    }

    IEnumerator PlaySoundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            audioSource.Play();
        }
    }
}
