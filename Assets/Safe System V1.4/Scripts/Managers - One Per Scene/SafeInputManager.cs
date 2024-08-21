/// <summary>
/// INPUT LOCATIONS IN THE: INTERACTOR & CONTROLLER & TRIGGER SCRIPTS
/// </summary>

using UnityEngine;

namespace SafeSystem
{
    public class SafeInputManager : MonoBehaviour
    {
        [Header("Safe Interaction")]
        public KeyCode openKey;

        [Header("Close Safe Interaction")]
        public KeyCode closeKey;

        [Header("Trigger Safe Interaction")]
        public KeyCode triggerInteractKey;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public static SafeInputManager instance;

        private void Awake()
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
    }
}
