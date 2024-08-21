using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonManagerNum : MonoBehaviour
{
    public Button[] buttons; // 5개의 버튼 배열
    public TextMeshProUGUI[] displayTexts; // 각 버튼의 변경된 텍스트를 표시할 TextMeshProUGUI 배열
    public TextMeshProUGUI finalDisplayText; // 5글자를 합쳐서 표시할 TextMeshProUGUI
    private string[,] texts = 
    {
        { "0","1", "2", "3", "4", "5","6", "7", "8", "9" },
        { "0","1", "2", "3", "4", "5","6", "7", "8", "9" },
        { "0","1", "2", "3", "4", "5","6", "7", "8", "9" }
        
    }; // 변경할 텍스트 배열
    private int[] currentIndices; // 각 버튼의 현재 인덱스를 저장할 배열
    private string[] collectedTexts; // 최종 텍스트를 저장할 배열

    void Start()
    {
        
        currentIndices = new int[buttons.Length];
        collectedTexts = new string[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; // 로컬 복사본을 만들어서 클로저 문제를 피함
            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        
        if (currentIndices[buttonIndex] < texts.GetLength(1))
        {
            string newText = texts[buttonIndex, currentIndices[buttonIndex]];
            UpdateDisplayText(buttonIndex, newText);
            
            TextMeshProUGUI buttonText = buttons[buttonIndex].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = ""; // 버튼 텍스트 비우기
            }

            collectedTexts[buttonIndex] = newText; // 텍스트 저장
            currentIndices[buttonIndex] = (currentIndices[buttonIndex] + 1) % texts.GetLength(1); // 인덱스를 순환하도록 업데이트
            UpdateFinalDisplayText();
        }
        else
        {
            Debug.LogError("currentIndices[buttonIndex] is out of range.");
        }
    }

    void UpdateDisplayText(int buttonIndex, string text)
    {
        if (buttonIndex < displayTexts.Length)
        {
            displayTexts[buttonIndex].text = text;
        }
        else
        {
            Debug.LogError("displayTexts index is out of range.");
        }
    }

    void UpdateFinalDisplayText()
    {
        finalDisplayText.text = string.Join("", collectedTexts);
    }
}