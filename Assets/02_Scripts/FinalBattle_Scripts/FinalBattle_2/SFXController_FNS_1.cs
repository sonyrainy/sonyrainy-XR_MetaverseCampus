using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController_FNS_1 : MonoBehaviour
{
    private AudioSource sfxSource;
    public AudioMixerGroup sfxMixerGroup; // 오디오 믹서 그룹
    private Dictionary<string, AudioClip> sfxClips;

    void Start()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.priority = 256; // 기본 우선 순위
        sfxSource.spatialBlend = 1.0f; // 3D 사운드
        sfxSource.outputAudioMixerGroup = sfxMixerGroup; // 오디오 믹서 그룹 할당

        // 효과음 클립을 딕셔너리에 추가
        sfxClips = new Dictionary<string, AudioClip>();
    }

    public void AddClip(string clipName, AudioClip clip)
    {
        if (!sfxClips.ContainsKey(clipName))
        {
            sfxClips.Add(clipName, clip);
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxClips.ContainsKey(sfxName))
        {
            sfxSource.PlayOneShot(sfxClips[sfxName]);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found!");
        }
    }
}
