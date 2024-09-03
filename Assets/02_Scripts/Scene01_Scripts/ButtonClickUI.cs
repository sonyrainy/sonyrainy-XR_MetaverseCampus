using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckManager : MonoBehaviour
{
    public TextMeshProUGUI finalDisplayText; // 5글자를 합쳐서 표시할 TextMeshProUGUI
    public Button checkButton; // 최종 텍스트를 확인할 버튼
    public string targetText; // 확인할 텍스트를 public 변수로 설정
    public HingeJoint boxHingeJoint; // 힌지 조인트를 참조할 필드
    public GameObject wall;
    public GameObject objectToActivate; // 활성화할 오브젝트

    void Start()
    {
        checkButton.onClick.AddListener(OnCheckButtonClick); // checkButton 클릭 이벤트 추가

        // 처음에는 특정 오브젝트를 비활성화
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void OnCheckButtonClick()
    {
        if (finalDisplayText.text == targetText)
        {
            // 특정 조건이 맞으면 수행할 작업
            Debug.Log("Final text is HELLO!");
            // 여기에 원하는 작업을 추가하세요.
            CorrectAnswer();
            wall.SetActive(false);
        }
        else
        {
            Debug.Log("Final text does not match.");
        }
    }

    private void CorrectAnswer()
    {
        // 힌지 조인트의 제한을 변경합니다.
        if (boxHingeJoint != null)
        {
            JointLimits limits = boxHingeJoint.limits;
            limits.max = 90; // max 값을 90으로 설정
            boxHingeJoint.limits = limits; // 변경된 제한을 힌지 조인트에 적용
        }

        // 비활성화된 오브젝트를 활성화합니다.
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
}
