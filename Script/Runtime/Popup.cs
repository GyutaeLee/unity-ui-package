using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace qbot.UI
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] protected Canvas PopupCanvas;
        [SerializeField] protected GameObject PopupPrefab;

        protected readonly HashSet<GameObject> InstantiatedPopupObjects = new();

        public GameObject InstantiateDefaultPopup(string description, string buttonDescription = "OK", UnityAction buttonUnityAction = null)
        {
            var popupObject = Instantiate(PopupPrefab, PopupCanvas.transform, true);
            InstantiatedPopupObjects.Add(popupObject);
            popupObject.SetActive(true);

            var descriptionText = popupObject.transform.Find("popupDescriptionText").GetComponent<TextMeshProUGUI>();
            if (descriptionText != null)
            {
                descriptionText.text = description;
            }

            var buttonObjectTransform = popupObject.transform.Find("popupButton");
            if (buttonObjectTransform != null)
            {
                var button = buttonObjectTransform.GetComponent<Button>();
                if (button != null)
                {
                    button.onClick.AddListener(buttonUnityAction ?? (() => { Destroy(popupObject); }));
                }
            }

            var buttonDescriptionText = buttonObjectTransform.Find("popupButtonText").GetComponent<TextMeshProUGUI>();
            if (buttonDescriptionText != null)
            {
                buttonDescriptionText.text = buttonDescription;
            }

            return popupObject;
        }

        public void CloseAllInstantiatedPopups()
        {
            foreach (var go in InstantiatedPopupObjects.Where(go => go != null))
            {
                Destroy(go);
            }

            InstantiatedPopupObjects.Clear();
        }
    }
}