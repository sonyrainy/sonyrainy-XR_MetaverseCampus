using UnityEngine;

public class LeverMove : MonoBehaviour
{
    public Light targetLight; // 켜야 할 라이트
    public GameObject objectToActivate; // 활성화할 비활성화된 오브젝트
    public float activationAngle = 50f; // 레버의 각도가 이 값 이상이면 활성화
    private HingeJoint hingeJoint; // Hinge Joint 컴포넌트
    private bool isActivated = false; // 레버가 이미 활성화되었는지 여부

    void Start()
    {
        // Hinge Joint 컴포넌트를 가져옵니다.
        hingeJoint = GetComponent<HingeJoint>();

        // 라이트를 초기에는 비활성화합니다.
        if (targetLight != null)
        {
            targetLight.enabled = false;
        }

        // 오브젝트를 초기에는 비활성화합니다.
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }
    }

    void Update()
    {
        // 현재 레버의 각도를 가져옵니다.
        float currentAngle = hingeJoint.angle;

        // 레버가 활성화 각도를 넘었는지 확인합니다.
        if (!isActivated && Mathf.Abs(currentAngle) >= activationAngle)
        {
            Activate();
        }
    }

    void Activate()
    {
        // 라이트를 켭니다.
        if (targetLight != null)
        {
            targetLight.enabled = true;
        }

        // 비활성화된 오브젝트를 활성화합니다.
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // 활성화 플래그를 설정합니다.
        isActivated = true;
    }
}
