using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

namespace qbot.UI
{
    public class Popup : MonoBehaviour
    {
#region Properties

        /// <summary>
        /// Popup object.
        /// </summary>
        [FormerlySerializedAs("popupObject")]
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
        [FormerlySerializedAs("popupButton")]
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
        [FormerlySerializedAs("popupDescriptionText")]
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
        [FormerlySerializedAs("buttonDescriptionText")]
        [SerializeField]
        private TextMeshProUGUI _buttonDescriptionText;
        public TextMeshProUGUI ButtonDescriptionText
        {
            private get => _buttonDescriptionText;
            set => _buttonDescriptionText = value;
        }

        private bool AllPopupClosed => _popupObject.activeSelf == false;

#endregion

#region Public functions

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

            if (buttonUnityAction != null)
            {
                _popupButton.onClick.AddListener(buttonUnityAction);
            }

            _popupButton.onClick.AddListener(CloseDefaultPopup);
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

#endregion
    }
}