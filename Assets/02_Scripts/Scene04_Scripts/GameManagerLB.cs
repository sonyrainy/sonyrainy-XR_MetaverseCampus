using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManagerLB : MonoBehaviour
{
    public GameObject[] objectsToActivate = new GameObject[5];
    public AudioClip selectSound;  // 재생할 사운드 클립

    // 이 메서드는 XRGrabInteractable의 selectEntered 이벤트에 연결됩니다.
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject selectedObject = args.interactableObject.transform.gameObject;

        // 오브젝트를 활성화합니다.
        for (int i = 0; i < 5; i++)
        {
            objectsToActivate[i].SetActive(true);
        }

        // selectSound가 할당된 경우 사운드를 재생합니다.
        if (selectSound != null)
        {
            // 오브젝트의 위치에서 사운드를 재생합니다.
            AudioSource.PlayClipAtPoint(selectSound, selectedObject.transform.position);
        }
    } 
}