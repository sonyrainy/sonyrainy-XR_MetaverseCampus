using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManagerLB : MonoBehaviour
{
    public GameObject[] objectsToActivate = new GameObject[5];

    // 이 메서드는 XRGrabInteractable의 selectEntered 이벤트에 연결됩니다.
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject selectedObject = args.interactableObject.transform.gameObject;

        for (int i = 0; i < 5; i++)
        {
            objectsToActivate[i].SetActive(true);
        }   
    } 
}  