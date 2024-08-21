using UnityEngine;

namespace SafeSystem
{
    public class SafeItem : MonoBehaviour
    {
        [SerializeField] private SafeController _safeController = null;

        public void ShowSafeLock()
        {
            _safeController.ShowSafeUI();
        }
    }
}
