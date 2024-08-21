using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckManagerNum : MonoBehaviour
{
    public TextMeshProUGUI finalDisplayTextNum; // 5글자를 합쳐서 표시할 TextMeshProUGUI
    public Button checkButtonNum; // 최종 텍스트를 확인할 버튼
    public string targetTextNum; // 확인할 텍스트를 public 변수로 설정
    public HingeJoint boxHingeJointNum; // 힌지 조인트를 참조할 필드
    public GameObject ButtonClickUINum;
    public AudioClip correctSound; // 정답일 때 재생할 사운드 클립
    private AudioSource audioSource; // 오디오 소스
    

    void Start()
    {
        checkButtonNum.onClick.AddListener(OnCheckButtonClickNum); // checkButton 클릭 이벤트 추가

        // 오디오 소스 설정
        audioSource = gameObject.AddComponent<AudioSource>();
        
    }

    void OnCheckButtonClickNum()
    {
        if (finalDisplayTextNum.text == targetTextNum)
        {
            
            CorrectAnswerNum();
            ButtonClickUINum.SetActive(false);

            // 정답 사운드 재생
            if (correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
            }
        }
        else
        {
            Debug.Log("Final text does not match.");
        }
    }

    private void CorrectAnswerNum()
    {
        // 힌지 조인트의 제한을 변경합니다.
        if (boxHingeJointNum != null)
        {
            JointLimits limits = boxHingeJointNum.limits;
            limits.max = 90; // max 값을 90으로 설정
            limits.min = 80;
            boxHingeJointNum.limits = limits; // 변경된 제한을 힌지 조인트에 적용
        }
    }
}