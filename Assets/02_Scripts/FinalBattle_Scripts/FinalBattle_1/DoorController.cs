using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float doorOpenDistanceX = 2f; // 문이 X축으로 열리는 거리
    public float doorOpenSpeed = 3f; // 문이 열리는 속도 (기본값을 느리게 조정)
    private bool isOpen = false; // 문이 열린 상태인지 여부

    public void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(Mathf.Abs(doorOpenDistanceX), 0, 0);

        float elapsedTime = 0f;

        while (elapsedTime < doorOpenSpeed)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / doorOpenSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
        isOpen = true; // 문이 열린 상태로 설정
    }
}
