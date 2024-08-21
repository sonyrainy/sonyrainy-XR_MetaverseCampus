using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    public XRSocketInteractor[] sockets;  // XR 소켓 배열
    private bool[] socketStatus;  // 각 소켓의 상태 추적
    public GameObject objectToMove;  // 이동시킬 물체
    
    public delegate void AllSocketsSelectedAction();
    public static event AllSocketsSelectedAction OnAllSocketsSelected;

    void Start()
    {
        socketStatus = new bool[sockets.Length];
        
        for (int i = 0; i < sockets.Length; i++)
        {
            int index = i;  // 클로저 문제 방지
            sockets[i].selectEntered.AddListener(args => OnSelectEntered(index));
            sockets[i].selectExited.AddListener(args => OnSelectExited(index));
        }
    }

    private void OnSelectEntered(int index)
    {
        socketStatus[index] = true;
        CheckAllSockets();
    }

    private void OnSelectExited(int index)
    {
        socketStatus[index] = false;
    }

    private void CheckAllSockets()
    {
        foreach (var status in socketStatus)
        {
            if (!status)
            {
                return;
            }
        }

        OnAllSocketsSelected?.Invoke();
        Debug.Log("All sockets are selected!");
        DestroyObject();
    }

    private void DestroyObject()
    {
            Destroy(objectToMove,2);
    }
}