using UnityEngine;

namespace SafeSystem
{
    public class SafeInteractor : MonoBehaviour
    {
        [Header("Raycast Distance")]
        [SerializeField] private int rayDistance = 5;

        [Header("Interact Tag")]
        [SerializeField] private string interactiveObjectTag = "InteractiveObject";

        private SafeItem safe;
        private Camera _camera;

        private void Awake()
        {
            if (!TryGetComponent<Camera>(out _camera))
            {
                Debug.LogError("Camera component not found on the GameObject.");
            }
        }

        private void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out hit, rayDistance))
            {
                var selectedItem = hit.collider.GetComponent<SafeItem>();
                if (selectedItem != null && selectedItem.CompareTag(interactiveObjectTag))
                {
                    safe = selectedItem;
                    HighlightCrosshair(true);
                }
                else
                {
                    ClearInteractor();
                }
            }
            else
            {
                ClearInteractor();
            }

            if (safe != null)
            {
                if (Input.GetKeyDown(SafeInputManager.instance.openKey))
                {
                    safe.ShowSafeLock();
                }
            }
        }

        private void ClearInteractor()
        {
            if (safe != null)
            {
                HighlightCrosshair(false);
                safe = null;
            }
        }

        void HighlightCrosshair(bool on)
        {
            SafeUIManager.instance.HighlightCrosshair(on);
        }
    }
}