using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace SafeSystem
{
    public class SafeUIManager : MonoBehaviour
    {
        [Space(5)][SerializeField] private CanvasGroup safeCanvasGroup = null;

        [Tooltip("Add the UI numbers text UI elements here")]
        [Space(5)][SerializeField] private Button acceptBtn = null;

        [Tooltip("Add the UI numbers text UI elements here")]
        [Space(5)][SerializeField] private TMP_Text[] numberUI = new TMP_Text[3];

        [Tooltip("Add the UI selection buttons, there should be 3")]
        [Space(5)][SerializeField] private Button[] selectionBtn = new Button[3];

        [Space(5)][SerializeField] private GameObject interactPrompt = null;

        [Space(5)][SerializeField] private Image crosshair = null;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        public string playerInputNumber { get; private set; }

        public static SafeUIManager instance;

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

            FieldNullCheck();
        }

        public void ShowMainSafeUI(bool active)
        {
            safeCanvasGroup.alpha = active ? 1 : 0;
            if(active)
            {
                SetInitialSafeUI();
            }
        }

        public void SetInitialSafeUI()
        {
            acceptBtn.onClick.RemoveAllListeners();
            foreach (var btn in selectionBtn) btn.onClick.RemoveAllListeners();
            ResetSafeUI();
        }

        public void ResetEventSystem()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void ResetSafeUI()
        {
            foreach (var numUI in numberUI)
            {
                numUI.text = "0";
            }
            UpdateUIState(0);
        }

        public void SetUIButtons(SafeController _myController)
        {
            acceptBtn.onClick.AddListener(_myController.CheckDialNumber);
            for (int i = 0; i < selectionBtn.Length; i++)
            {
                int index = i + 1;
                selectionBtn[i].onClick.AddListener(() => _myController.MoveDialLogic(index));
            }
        }

        public void PlayerInputCode()
        {
            playerInputNumber = string.Join("", numberUI[0].text, numberUI[1].text, numberUI[2].text);
        }

        public void UpdateNumber(int index, int lockNumber)
        {
            if (index >= 0 && index < numberUI.Length)
            {
                numberUI[index].text = lockNumber.ToString();
            }
            else
            {
                Debug.LogError("Invalid index for UpdateNumber: " + index);
            }
        }

        public void UpdateUIState(int index)
        {
            for (int i = 0; i < numberUI.Length; i++)
            {
                selectionBtn[i].interactable = (i == index);
                numberUI[i].color = (i == index) ? Color.white : Color.gray;

                ColorBlock arrowCB = selectionBtn[i].colors;
                arrowCB.normalColor = (i == index) ? Color.white : Color.gray;
                selectionBtn[i].colors = arrowCB;
            }
        }

        public void SetInteractPrompt(bool on)
        {
            interactPrompt.SetActive(on);
        }

        public void HighlightCrosshair(bool on)
        {
            crosshair.color = on ? Color.red : Color.white;
        }

        public void DisableCrosshair(bool disable)
        {
            Cursor.lockState = disable ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = disable;
            crosshair.enabled = !disable;
        }

        void FieldNullCheck()
        {
            // Checking each field and logging an error if it is null
            CheckField(safeCanvasGroup, "SafeCanvasGroup");
            CheckField(acceptBtn, "AcceptBtn");

            // Checking each element in the numberUI array
            for (int i = 0; i < numberUI.Length; i++)
            {
                CheckField(numberUI[i], $"NumberUI[{i}]");
            }

            // Checking each button in the selectionBtn array
            for (int i = 0; i < selectionBtn.Length; i++)
            {
                CheckField(selectionBtn[i], $"SelectionBtn[{i}]");
            }

            CheckField(interactPrompt, "InteractPrompt");
            CheckField(crosshair, "Crosshair");
        }

        void CheckField(Object field, string fieldName)
        {
            if (field == null)
            {
                Debug.LogError(gameObject + $"FieldNullCheck: {fieldName} is not set in the inspector!");
            }
        }
    }
}
