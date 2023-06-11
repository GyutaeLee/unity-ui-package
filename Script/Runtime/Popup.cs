using UnityEngine;
using System.Collections;
using System;
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
        [SerializeField] private GameObject popupObject;
        public GameObject PopupObject
        {
            private get
            {
                return popupObject;
            }
            set
            {
                popupObject = value;
            }
        }

        /// <summary>
        /// Popup button.
        /// </summary>
        [SerializeField] private Button popupButton;
        public Button PopupButton
        {
            private get
            {
                return popupButton;
            }
            set
            {
                popupButton = value;
            }
        }

        /// <summary>
        /// Popup main description.
        /// </summary>
        [SerializeField] private TextMeshProUGUI popupDescriptionText;
        public TextMeshProUGUI PopupDescriptionText
        {
            private get
            {
                return popupDescriptionText;
            }
            set
            {
                popupDescriptionText = value;
            }
        }

        /// <summary>
        /// Button main description.
        /// </summary>
        [SerializeField] private TextMeshProUGUI buttonDescriptionText;
        public TextMeshProUGUI ButtonDescriptionText
        {
            private get
            {
                return buttonDescriptionText;
            }
            set
            {
                popupDescriptionText = value;
            }
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Opens the one button popup.
        /// </summary>
        /// <param name="popUpDescriptionText">Text for the popup</param>
        /// <param name="buttonDescriptionText">Text for the button</param>
        /// <param name="buttonUnityAction">The action that will be called when the button is pressed</param>
        public void OpenDefaultPopup(string popUpDescriptionText, string buttonDescriptionText, UnityAction buttonUnityAction = null)
        {
            if (this.popupObject == null || this.popupButton == null || this.PopupDescriptionText == null || this.destroyCancellationToken == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            this.popupObject.SetActive(true);

            this.popupDescriptionText.text = popUpDescriptionText;
            this.buttonDescriptionText.text = buttonDescriptionText;

            if (buttonUnityAction != null)
            {
                this.popupButton.onClick.AddListener(buttonUnityAction);
            }

            this.popupButton.onClick.AddListener(CloseDefaultPopup);
        }

        /// <summary>
        /// Close the one button popup.
        /// </summary>
        public void CloseDefaultPopup()
        {
            if (this.popupObject == null || this.popupButton == null)
            {
                Debug.Log("Something is null.");
                return;
            }

            this.popupObject.SetActive(false);
            this.popupButton.onClick.RemoveAllListeners();
        }
        #endregion
    }
}
