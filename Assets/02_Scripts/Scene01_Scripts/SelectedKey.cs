using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEventHandler : MonoBehaviour
{
    public HingeJoint boxHingeJoint; // 힌지 조인트를 참조할 필드
    public GameObject LineObject;

    private void Start()
    {
        // 힌지 조인트의 초기값을 설정해줍니다.
        if (boxHingeJoint != null)
        {
            JointLimits initialLimits = boxHingeJoint.limits;
            initialLimits.max = 0; // max 값을 0으로 설정해서 상자가 시작 시 열리지 않게 설정
            boxHingeJoint.limits = initialLimits;
        }
        
        // LineObject를 비활성화해서 상자가 열리기 전까지는 보이지 않게 설정
        if (LineObject != null)
        {
            LineObject.SetActive(false);
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // 선택된 오브젝트를 가져옵니다.
        GameObject keyObject = args.interactableObject.transform.gameObject;
        
        // 힌지 조인트의 제한을 변경합니다.
        if (boxHingeJoint != null)
        {
            JointLimits limits = boxHingeJoint.limits;
            limits.max = 90; // max 값을 90으로 설정해서 상자가 열리게 만듭니다.
            boxHingeJoint.limits = limits; // 변경된 제한을 힌지 조인트에 적용
        }
        
        // LineObject를 활성화해서 상자가 열리는 동안 보여줍니다.
        if (LineObject != null)
        {
            LineObject.SetActive(true);
        }
        
        // 2초 후에 열쇠와 소켓을 모두 삭제합니다.
        Destroy(keyObject, 2f); 
        Destroy(gameObject, 2f); // 현재 게임 오브젝트(소켓)를 삭제합니다.
    }
}
