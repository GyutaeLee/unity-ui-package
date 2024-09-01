using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace qbot.UI
{
    public class Popup : MonoBehaviour
    {
        /// <summary>
        /// Popup object.
        /// </summary>
        [SerializeField]
        private GameObject _popupObject;
        public GameObject PopupObject
        {
            private get => _popupObject;
            set => _popupObject = value;
        }

        /// <summary>
        /// Popup button.
        /// </summary>
        [SerializeField]
        private Button _popupButton;
        public Button PopupButton
        {
            private get => _popupButton;
            set => _popupButton = value;
        }

        /// <summary>
        /// Popup main description.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _popupDescriptionText;
        public TextMeshProUGUI PopupDescriptionText
        {
            private get => _popupDescriptionText;
            set => _popupDescriptionText = value;
        }

        /// <summary>
        /// Button main description.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _buttonDescriptionText;
        public TextMeshProUGUI ButtonDescriptionText
        {
            private get => _buttonDescriptionText;
            set => _buttonDescriptionText = value;
        }

        private bool AllPopupClosed => _popupObject.activeSelf == false;

        /// <summary>
        /// Opens the one button popup.
        /// </summary>
        /// <param name="popUpDescription">Text for the popup</param>
        /// <param name="buttonDescription">Text for the button</param>
        /// <param name="buttonUnityAction">The action that will be called when the button is pressed</param>
        public void OpenDefaultPopup(string popUpDescription, string buttonDescription = "OK", UnityAction buttonUnityAction = null)
        {
            if (_popupObject == null || _popupButton == null || PopupDescriptionText == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            _popupObject.SetActive(true);

            _popupDescriptionText.text = popUpDescription;
            _buttonDescriptionText.text = buttonDescription;

            _popupButton.onClick.AddListener(buttonUnityAction ?? CloseDefaultPopup);
        }

        /// <summary>
        /// Close the one button popup.
        /// </summary>
        public void CloseDefaultPopup()
        {
            if (_popupObject == null || _popupButton == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            _popupObject.SetActive(false);
            _popupButton.onClick.RemoveAllListeners();
        }
    }
}