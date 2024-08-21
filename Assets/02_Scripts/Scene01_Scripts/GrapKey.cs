using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrapKey : MonoBehaviour
{
    public GameObject ButtonClickUINum;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // UI를 활성화합니다.
        ButtonClickUINum.SetActive(true);
        
        // 잡힌 물체를 제거합니다.
        GameObject grabbedObject = args.interactableObject.transform.gameObject;
        Destroy(grabbedObject);
    }
}
