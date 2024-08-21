using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; 

public class BGM_Player_FNS_1 : MonoBehaviour
{
    public AudioClip bgmClip;
    public AudioMixerGroup bgmMixerGroup; // 오디오 믹서 그룹
    private AudioSource bgmSource;

    void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip = bgmClip;
        bgmSource.loop = true;
        bgmSource.playOnAwake = true;
        bgmSource.priority = 128; // 낮은 우선 순위
        bgmSource.spatialBlend = 0.0f; // 2D 사운드
        bgmSource.outputAudioMixerGroup = bgmMixerGroup; // 오디오 믹서 그룹 할당
        bgmSource.volume = 0.5f; // 볼륨 조정
        bgmSource.Play();
    }
}