using UnityEngine;

public class TeamTrigger : MonoBehaviour
{
    public GameObject objectToActivate; // 활성화할 오브젝트

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("PLAYER"))
        {
            Debug.Log("Player detected. Destroying trigger object and activating another object.");

            // 특정 오브젝트 활성화
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log(objectToActivate.name + " is now active.");
            }

            // 충돌한 오브젝트를 삭제
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (gameObject == null)
        {
            Debug.LogWarning("The object was destroyed");
        }
    }
}
