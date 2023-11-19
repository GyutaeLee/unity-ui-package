using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace qbot.UI
{
    public class Popup : MonoBehaviour
    {
#region Properties

        /// <summary>
        /// Popup object.
        /// </summary>
        [SerializeField]
        private GameObject popupObject;
        public GameObject PopupObject
        {
            private get => popupObject;
            set => popupObject = value;
        }

        /// <summary>
        /// Popup button.
        /// </summary>
        [SerializeField]
        private Button popupButton;
        public Button PopupButton
        {
            private get => popupButton;
            set => popupButton = value;
        }

        /// <summary>
        /// Popup main description.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI popupDescriptionText;
        public TextMeshProUGUI PopupDescriptionText
        {
            private get => popupDescriptionText;
            set => popupDescriptionText = value;
        }

        /// <summary>
        /// Button main description.
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI buttonDescriptionText;
        public TextMeshProUGUI ButtonDescriptionText
        {
            private get => buttonDescriptionText;
            set => buttonDescriptionText = value;
        }

        private bool AllPopupClosed => popupObject.activeSelf == false;

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
            if (popupObject == null || popupButton == null || PopupDescriptionText == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            popupObject.SetActive(true);

            popupDescriptionText.text = popUpDescription;
            buttonDescriptionText.text = buttonDescription;

            if (buttonUnityAction != null)
            {
                popupButton.onClick.AddListener(buttonUnityAction);
            }

            popupButton.onClick.AddListener(CloseDefaultPopup);
        }

        /// <summary>
        /// Close the one button popup.
        /// </summary>
        public void CloseDefaultPopup()
        {
            if (popupObject == null || popupButton == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            popupObject.SetActive(false);
            popupButton.onClick.RemoveAllListeners();
        }

#endregion
    }
}