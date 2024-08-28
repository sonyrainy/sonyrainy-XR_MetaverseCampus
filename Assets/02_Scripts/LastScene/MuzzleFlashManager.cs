using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MuzzleFlashManager : MonoBehaviour
{
    public string muzzleFlashTag = "MuzzleFlash"; // 총구 화염 오브젝트들의 태그
    public float flashDuration = 0.3f; // 총구 화염이 활성화되는 시간 (초 단위)
    public AudioClip flashSound; // 총구 화염 사운드 클립
    public float minSoundInterval = 0.5f; // 사운드 재생 최소 간격
    public float maxSoundInterval = 1.0f; // 사운드 재생 최대 간격
    public string FriendlyTag = "FRIENDLY"; // 적 오브젝트들의 태그

    private List<MeshRenderer> muzzleFlashes = new List<MeshRenderer>();
    private AudioSource audioSource;


    void Start()
    {
        // 태그로 모든 총구 화염 오브젝트를 불러와 리스트에 추가
        GameObject[] flashes = GameObject.FindGameObjectsWithTag(muzzleFlashTag);

        foreach (GameObject flashObj in flashes)
        {
            MeshRenderer flashRenderer = flashObj.GetComponent<MeshRenderer>();
            if (flashRenderer != null)
            {
                muzzleFlashes.Add(flashRenderer);
                flashRenderer.enabled = false; // 모든 총구 화염을 초기 상태에서 비활성화
            }
        }

        // AudioSource 컴포넌트 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = flashSound;
        audioSource.playOnAwake = false;
    }

    public void StartContinuousFlash()
    {
        
        StartCoroutine(ContinuousFlashCoroutine());
        
    }

    private IEnumerator ContinuousFlashCoroutine()
    {

         while (true)
        {
            TriggerMuzzleFlash();
            yield return new WaitForSeconds(Random.Range(minSoundInterval, maxSoundInterval));
        }
        
    }

    public void TriggerMuzzleFlash()
{
    if (muzzleFlashes.Count > 0)
    {
        // 랜덤하게 하나의 총구 화염 선택
        int randomIndex = Random.Range(0, muzzleFlashes.Count);
        MeshRenderer selectedFlash = muzzleFlashes[randomIndex];

        // 부모 오브젝트의 애니메이션 트리거
        TriggerParentAnimation(selectedFlash);

        // 일정 시간 후에 총구 화염 및 사운드 발생
        StartCoroutine(TriggerFlashWithDelay(selectedFlash));
    }
}

private void TriggerParentAnimation(MeshRenderer selectedFlash)
{
    // 부모 오브젝트의 Animator 가져오기
    Animator parentAnimator = selectedFlash.GetComponentInParent<Animator>();
    if (parentAnimator != null)
    {
        parentAnimator.SetBool("IsGunShoot", true); // 애니메이션 트리거
        StartCoroutine(ResetTrigger(parentAnimator)); // 일정 시간 후 다시 false로 설정
    }
}

private IEnumerator TriggerFlashWithDelay(MeshRenderer selectedFlash)
{
    // 애니메이션 동작 후 잠시 대기 (0.1초 정도)
    yield return new WaitForSeconds(0.1f);

    // 총구 화염 활성화
    selectedFlash.enabled = true;

    // 일정 시간 후 비활성화
    StartCoroutine(DisableFlashAfterDuration(selectedFlash));

    // 즉시 사운드 재생
    PlayFlashSound();
}

private IEnumerator ResetTrigger(Animator animator)
{
    yield return new WaitForSeconds(0.5f); // 애니메이션 재생 후 대기 시간
    animator.SetBool("IsGunShoot", false); // 애니메이션 종료
}

    private IEnumerator DisableFlashAfterDuration(MeshRenderer flash)
    {
        yield return new WaitForSeconds(flashDuration);  // 일정 시간 기다림
        flash.enabled = false;
    }

    private void PlayFlashSound()
    {
        if (audioSource != null && flashSound != null)
        {
            audioSource.Play();
        }
    }
}

