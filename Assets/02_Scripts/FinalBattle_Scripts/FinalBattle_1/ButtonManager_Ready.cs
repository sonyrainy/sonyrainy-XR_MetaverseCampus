using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ButtonManager_Ready : MonoBehaviour
{
    public Button[] buttons; // 3개의 버튼 배열
    public TextMeshProUGUI[] displayTexts; // 각 버튼의 변경된 텍스트를 표시할 TextMeshProUGUI 배열
    public TextMeshProUGUI finalDisplayText; // 3글자를 합쳐서 표시할 TextMeshProUGUI
    public GameObject box; // 열릴 상자 오브젝트
    public GameObject door; // 문 오브젝트
    public GameObject inactiveObject_1; // 활성화할 비활성화된 오브젝트1
    public GameObject inactiveObject_2; // 활성화할 비활성화된 오브젝트2

    public string correctCode = "123"; // 올바른 코드
    public Button checkButton; // 체크 버튼

    private int[] currentIndices; // 각 버튼의 현재 인덱스를 저장할 배열
    private string[] collectedTexts; // 최종 텍스트를 저장할 배열

    void Start()
    {
        // buttons.Length가 3이 아닐 경우 오류 메시지 출력
        if (buttons.Length != 3 || displayTexts.Length != 3)
        {
            Debug.LogError("Buttons and displayTexts arrays must contain exactly 3 elements.");
            return;
        }

        currentIndices = new int[buttons.Length];
        collectedTexts = new string[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // 로컬 복사본을 만들어서 클로저 문제를 피함
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }

        if (checkButton != null)
        {
            checkButton.onClick.AddListener(OnCheckButtonClick);
        }
        else
        {
            Debug.LogError("Check button is not assigned.");
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
        {
            Debug.LogError("Button index is out of range.");
            return;
        }

        currentIndices[buttonIndex] = (currentIndices[buttonIndex] + 1) % 9;
        string newText = (currentIndices[buttonIndex] + 1).ToString();
        UpdateDisplayText(buttonIndex, newText);

        TextMeshProUGUI buttonText = buttons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = ""; // 버튼 텍스트 비우기
        }

        collectedTexts[buttonIndex] = newText; // 텍스트 저장
        UpdateFinalDisplayText();
    }

    void OnCheckButtonClick()
    {
        if (finalDisplayText.text == correctCode)
        {
            OpenBoxAndDoor();
            ActivateObject();
        }
        else
        {
            Debug.Log("Incorrect code entered.");
        }
    }

    void UpdateDisplayText(int buttonIndex, string text)
    {
        if (buttonIndex < displayTexts.Length && displayTexts[buttonIndex] != null)
        {
            displayTexts[buttonIndex].text = text;
        }
        else
        {
            Debug.LogError("displayTexts index is out of range or is null.");
        }
    }

    void UpdateFinalDisplayText()
    {
        if (finalDisplayText != null)
        {
            finalDisplayText.text = string.Join("", collectedTexts);
        }
        else
        {
            Debug.LogError("finalDisplayText is null.");
        }
    }

    void OpenBoxAndDoor()
    {
        if (box != null)
        {
            BoxController boxController = box.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxController.OpenBox();
            }
            else
            {
                Debug.LogError("BoxController component is missing on box.");
            }
        }
        else
        {
            Debug.LogError("Box is null.");
        }

        if (door != null)
        {
            DoorController doorController = door.GetComponent<DoorController>();
            if (doorController != null)
            {
                doorController.OpenDoor();
            }
            else
            {
                Debug.LogError("DoorController component is missing on door.");
            }
        }
        else
        {
            Debug.LogError("Door is null.");
        }
    }

    void ActivateObject()
    {
        if (inactiveObject_1 != null)
        {
            inactiveObject_1.SetActive(true);
            inactiveObject_2.SetActive(true);
        }
        else
        {
            Debug.LogError("Inactive object is not assigned.");
        }
    }
}
