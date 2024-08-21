using UnityEngine;

namespace SafeSystem
{
    public class SafeTrigger : MonoBehaviour
    {
        [Header("Safe Controller Object")]
        [SerializeField] private SafeItem _safeItemController = null;

        [Space(10)]
        [SerializeField] private string playerTag = "Player";

        private bool canUse;

        private void Update()
        {
            ShowSafeInput();
        }

        void ShowSafeInput()
        {
            if (canUse && Input.GetKeyDown(SafeInputManager.instance.triggerInteractKey))
            {
                SafeUIManager.instance.SetInteractPrompt(false);
                _safeItemController.ShowSafeLock();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = true;
                SafeUIManager.instance.SetInteractPrompt(canUse);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                canUse = false;
                SafeUIManager.instance.SetInteractPrompt(canUse);
            }
        }
    }
}
