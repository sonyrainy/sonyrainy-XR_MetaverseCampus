using UnityEngine;
using UnityEngine.UI;

public class BoxUnlocker : MonoBehaviour
{
    public GameObject uiPanel; // UI 패널
    public InputField inputField; // 숫자 입력 필드
    public Button confirmButton; // 확인 버튼
    private int correctNumber = 1234; // 정답 숫자

    void Start()
    {
        uiPanel.SetActive(false); // 처음엔 UI 비활성화
        confirmButton.onClick.AddListener(CheckCode);
    }

    void OnMouseDown()
    {
        uiPanel.SetActive(true); // 상자를 클릭하면 UI 활성화
    }

    void CheckCode()
    {
        int enteredNumber;
        if (int.TryParse(inputField.text, out enteredNumber))
        {
            if (enteredNumber == correctNumber)
            {
                UnlockBox();
            }
            else
            {
                Debug.Log("Wrong number!");
            }
        }
    }

    void UnlockBox()
    {
        Debug.Log("Box unlocked!");
        uiPanel.SetActive(false);
        // 상자가 풀리는 애니메이션 또는 로직 추가
    }
}
