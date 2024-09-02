using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEventHandler : MonoBehaviour
{
    public HingeJoint boxHingeJoint; // 힌지 조인트를 참조할 필드
    public GameObject LineObject;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 선택된 오브젝트를 가져옵니다.
        GameObject keyObject = args.interactableObject.transform.gameObject;
        
        // 힌지 조인트의 제한을 변경합니다.
        if (boxHingeJoint != null)
        {
            JointLimits limits = boxHingeJoint.limits;
            limits.max = 90; // max 값을 90으로 설정
            boxHingeJoint.limits = limits; // 변경된 제한을 힌지 조인트에 적용
            
        }
        
        LineObject.SetActive(true);
        
        // 2초 후에 열쇠와 소켓을 모두 삭제합니다.
        Destroy(keyObject, 2f); 
        Destroy(gameObject, 2f); // 현재 게임 오브젝트(소켓)를 삭제합니다.
    }
}
