using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrapRedKey : MonoBehaviour
{
    public HingeJoint DoorHingeJoint1;
    public HingeJoint DoorHingeJoint2;
    public AudioClip grabSound; // 키를 잡을 때 재생할 사운드 클립
    private AudioSource audioSource; // 오디오 소스

    void Start()
    {
        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("잡혔다");
        GrapKey();
        GrapKey2();

        
        // 사운드 재생
        if (grabSound != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }
    
    private void GrapKey()
    {
        JointLimits limits = DoorHingeJoint1.limits;
        limits.min = -97;
        limits.max = -96; // max 값을 -96으로 설정
        DoorHingeJoint1.limits = limits; // 변경된 제한을 힌지 조인트에 적용
    }

    private void GrapKey2()
    {
        JointLimits limits = DoorHingeJoint2.limits;
        limits.max = 97;
        limits.min = 96; // min 값을 96으로 설정
        DoorHingeJoint2.limits = limits; // 변경된 제한을 힌지 조인트에 적용
    }
}
