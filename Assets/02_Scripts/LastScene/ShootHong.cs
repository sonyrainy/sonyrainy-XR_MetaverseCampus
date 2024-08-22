using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ShootHong : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 총알 프리팹
    public Transform firePoint; // 총알이 발사될 위치
    public float bulletSpeed = 20f; // 총알 속도
    private MeshRenderer muzzleFlash;

    void Start()
    {
        muzzleFlash = firePoint.GetComponentInChildren<MeshRenderer>();  //자식 컴포넌트를 얻어온다
        muzzleFlash.enabled = false;  //안보이게한다
        // 만약 XRGrabInteractable 컴포넌트를 사용하는 경우, 이벤트를 직접 등록할 수 있습니다.
        var interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.activated.AddListener(OnActivated); // Activate 이벤트에 리스너 추가
        }
    }

    // Activate 이벤트가 발생할 때 호출되는 함수
    void OnActivated(ActivateEventArgs args)
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        // 총알 프리팹 생성 및 발사
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
         StartCoroutine(ShowMuzzleFlash()); //총구화염효과 코루틴함수 호출

        // 일정 시간 후 총알 제거
        Destroy(bullet, 2.0f);
    }

    IEnumerator ShowMuzzleFlash() {  //코루틴
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;  //4장중 한장을 선택
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0,0, angle);  //로컬회전, 쿼터니언으로 변경할것
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;  //로컬스케일변경
        muzzleFlash.enabled=true; //muzzleFlash 활성화
        yield return new WaitForSeconds(0.2f);  // new를 붙여줘야함. 권한을 0.2초 넘겨줌
        muzzleFlash.enabled=false;  //muzzleFlash 비활성화
    }
}
