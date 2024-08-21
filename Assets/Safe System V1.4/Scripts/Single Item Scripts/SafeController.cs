using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SafeSystem
{
    public class SafeController : MonoBehaviour
    {
        [Header("Safe Model References")]
        [SerializeField] private GameObject safeModel = null;
        [SerializeField] private Transform safeDial = null;

        [Header("Animation Reference")]
        [SerializeField] private string safeAnimationName = "SafeDoorOpen";

        [Header("Animation Timers")]
        [SerializeField] private float beforeAnimationStart = 1.0f; //Default: 1.0f
        [SerializeField] private float beforeOpenDoor = 0.5f; //Default: 0.5

        [Header("Safe Solution: 0-15")]
        [Range(0, 15)][SerializeField] private int safeSolutionNum1 = 0;
        [Range(0, 15)][SerializeField] private int safeSolutionNum2 = 0;
        [Range(0, 15)][SerializeField] private int safeSolutionNum3 = 0;

        [Header("Trigger Interaction?")]
        [SerializeField] private bool isTriggerInteraction = false;
        [SerializeField] private GameObject triggerObject = null;

        [Header("Audio ScriptableObjects")]
        [SerializeField] private SafeAudioClips _safeAudioClips = null;

        [Header("Unity Event - What happens when you open the safe?")]
        [SerializeField] private UnityEvent safeOpened = null;

        private int lockState;
        private bool canClose = false;
        private bool isInteracting = false;
        private Animator safeAnim;
        private int[] currentLockNumbers = new int[3];
        private int currentLockNumber;


        void Awake()
        {
            safeAnim = safeModel.gameObject.GetComponent<Animator>();

            for (int i = 0; i < currentLockNumbers.Length; i++)
                currentLockNumbers[i] = 0;
        }

        public void ShowSafeUI()
        {
            if (isTriggerInteraction)
            {
                canClose = false;
                triggerObject.SetActive(false);
            }

            isInteracting = true;
            lockState = 1;
            SafeUIManager.instance.ShowMainSafeUI(true);
            SafeDisableManager.instance.DisablePlayer(true);
            SafeUIManager.instance.SetUIButtons(this);
            PlayInteractSound();
        }

        private void Update()
        {
            if (!canClose && isInteracting && Input.GetKeyDown(SafeInputManager.instance.closeKey))
            {
                CloseSafeUI();
            }
        }

        private void CloseSafeUI()
        {
            if (isTriggerInteraction)
            {
                canClose = true;
                triggerObject.SetActive(true);
            }

            SafeDisableManager.instance.DisablePlayer(false);
            ResetSafeDial(false);
            SafeUIManager.instance.ShowMainSafeUI(false);
            isInteracting = false;
        }

        void ResetSafeDial(bool hasComplete)
        {
            if (!hasComplete)
            {
                PlayRattleSound();
            }

            lockState = 1;

            SafeUIManager.instance.ResetSafeUI();
            safeDial.transform.localEulerAngles = Vector3.zero;

            // Reset the current lock number and all the current lock numbers.
            currentLockNumber = 0;
            for (int i = 0; i < currentLockNumbers.Length; i++)
            {
                currentLockNumbers[i] = 0;
            }
        }

        private IEnumerator CheckCode()
        {
            SafeUIManager.instance.PlayerInputCode();
            string safeSolution = $"{safeSolutionNum1}{safeSolutionNum2}{safeSolutionNum3}";

            if (SafeUIManager.instance.playerInputNumber == safeSolution)
            {
                SafeDisableManager.instance.DisablePlayer(false);
                SafeUIManager.instance.ShowMainSafeUI(false);
                isInteracting = false;
                safeModel.tag = "Untagged";

                PlayBoltUnlockSound();
                yield return new WaitForSeconds(beforeAnimationStart);
                safeAnim.Play(safeAnimationName, 0, 0.0f);
                PlayHandleSpinSound();
                yield return new WaitForSeconds(beforeOpenDoor);
                PlayDoorOpenSound();

                if (isTriggerInteraction)
                {
                    canClose = true;
                    triggerObject.SetActive(false);
                }

                ResetSafeDial(true);
                safeOpened.Invoke();
            }
            else
            {
                ResetSafeDial(false);
            }
        }

        public void CheckDialNumber()
        {
            SafeUIManager.instance.ResetEventSystem();
            PlayInteractSound();

            // Save the current lock number before switching.
            currentLockNumbers[lockState - 1] = currentLockNumber;

            if (lockState < 3)
            {
                SafeUIManager.instance.UpdateUIState(lockState);
                currentLockNumbers[lockState] = currentLockNumber;
                lockState++;
            }
            else
            {
                SafeUIManager.instance.UpdateUIState(3);
                StartCoroutine(CheckCode());
                lockState = 1;
            }

            // After switching, set the current lock number to the saved value for this lock state.
            currentLockNumber = currentLockNumbers[lockState - 1];
            SafeUIManager.instance.UpdateNumber(lockState - 1, currentLockNumber);
        }

        public void MoveDialLogic(int lockNumberSelection)
        {
            SafeUIManager.instance.ResetEventSystem();
            PlaySafeClickSound();

            if (lockNumberSelection == 1 || lockNumberSelection == 3)
            {
                currentLockNumber = (currentLockNumber + 1) % 16;
                currentLockNumbers[lockState - 1] = currentLockNumber;
                RotateDial(false);
            }
            else if (lockNumberSelection == 2)
            {
                currentLockNumber = (currentLockNumber + 15) % 16;
                currentLockNumbers[lockState - 1] = currentLockNumber;
                RotateDial(true);
            }

            SafeUIManager.instance.UpdateNumber(lockState - 1, currentLockNumber);
        }

        void RotateDial(bool positive)
        {
            if (positive)
            {
                safeDial.transform.Rotate(0.0f, -22.5f, 0.0f, Space.Self);
            }
            else
            {
                safeDial.transform.Rotate(0.0f, 22.5f, 0.0f, Space.Self);
            }
        }

        void PlayInteractSound()
        {
            _safeAudioClips.PlayInteractSound();
        }

        void PlayBoltUnlockSound()
        {
            _safeAudioClips.PlayBoltUnlockSound();
        }

        void PlayHandleSpinSound()
        {
            _safeAudioClips.PlayHandleSpinSound();
        }

        void PlayDoorOpenSound()
        {
            _safeAudioClips.PlayDoorOpenSound();
        }

        void PlayRattleSound()
        {
            _safeAudioClips.PlayRattleSound();
        }

        void PlaySafeClickSound()
        {
            _safeAudioClips.PlaySafeClickSound();
        }
    }
}