using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace SafeSystem
{
    public class SafeDisableManager : MonoBehaviour
    {
        [Header("Player Controller Reference")]
        [SerializeField] private FirstPersonController player = null;

        [Header("Raycast Reference")]
        [SerializeField] private SafeInteractor safeRaycast = null;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public static SafeDisableManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        public void DisablePlayer(bool disable)
        {
            SafeUIManager.instance.DisableCrosshair(disable);
            if (disable)
            {
                player.enabled = false;
                safeRaycast.enabled = false;
            }
            else
            {
                player.enabled = true;
                safeRaycast.enabled = true;
            }
        }
    }
}
