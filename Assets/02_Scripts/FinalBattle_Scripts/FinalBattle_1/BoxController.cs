using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public Transform lid; // 뚜껑 오브젝트
    public float openAngle = 50f; // 뚜껑이 열리는 각도
    public float openSpeed = 1f; // 뚜껑이 열리는 속도
    private bool isOpen = false; // 박스가 열린 상태인지 여부

    public void OpenBox()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenLid());
        }
    }

    private IEnumerator OpenLid()
    {
        Quaternion startRotation = lid.localRotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, openAngle, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < openSpeed)
        {
            lid.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / openSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lid.localRotation = endRotation;
        isOpen = true; // 박스가 열린 상태로 설정
    }
}
