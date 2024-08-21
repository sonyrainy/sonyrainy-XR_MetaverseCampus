using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeySocketInteractor : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public GameObject door;
    public float doorOpenSpeed = 1f; // 문이 열리는 속도

    private void Start()
    {
        if (socketInteractor == null)
        {
            socketInteractor = GetComponent<XRSocketInteractor>();
        }

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnKeyInserted);
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnKeyInserted);
        }
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        // Check if the inserted object has the tag "KEY"
        if (args.interactableObject.transform.CompareTag("KEY"))
        {
            // Start the coroutine to smoothly rotate the door
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        Vector3 startRotation = door.transform.localEulerAngles;
        Vector3 endRotation = new Vector3(startRotation.x, startRotation.y, startRotation.z - 60);
        float elapsedTime = 0f;

        while (elapsedTime < doorOpenSpeed)
        {
            door.transform.localEulerAngles = Vector3.Lerp(startRotation, endRotation, elapsedTime / doorOpenSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the door reaches the final rotation
        door.transform.localEulerAngles = endRotation;
    }
}
