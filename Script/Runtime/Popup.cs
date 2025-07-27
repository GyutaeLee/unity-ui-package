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
        [SerializeField] protected Canvas _popupCanvas;
        [SerializeField] private GameObject _popupObject;

        protected readonly HashSet<GameObject> InstantiatedPopupObjects = new();

        public GameObject InstantiateDefaultPopup(string description, string buttonDescription = "OK", UnityAction buttonUnityAction = null)
        {
            var popupObject = Instantiate(_popupObject, _popupCanvas.transform, true);
            InstantiatedPopupObjects.Add(popupObject);
            popupObject.SetActive(true);

            var descriptionText = popupObject.transform.Find("popupDescriptionText").GetComponent<TextMeshProUGUI>();
            descriptionText.text = description;

            var buttonObjectTransform = popupObject.transform.Find("popupButton");
            var button = buttonObjectTransform.GetComponent<Button>();
            button.onClick.AddListener(buttonUnityAction ?? (() => { Destroy(popupObject); }));

            var buttonDescriptionText = buttonObjectTransform.Find("popupButtonText").GetComponent<TextMeshProUGUI>();
            buttonDescriptionText.text = buttonDescription;

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