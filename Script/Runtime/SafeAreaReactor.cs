using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace qbot.UI
{
    /// <summary>
    /// Add the SafeAreaReactor class as a Component to an object that needs SafeArea processing,
    /// it moves the object's anchor by the SafeArea range.
    /// </summary>
    public class SafeAreaReactor : MonoBehaviour
    {
        #region Fields
        [SerializeField] private RectTransform rectTransform;
        #endregion

        #region Monobehaviour functions
        private void Start()
        {
            PrepareBaseObjects();
            ApplySafeAreaPosition();
        }
        #endregion

        #region Private functions
        private void PrepareBaseObjects()
        {
            if (this.rectTransform == null)
            {
                this.rectTransform = this.gameObject.GetComponent<RectTransform>();
            }
        }

        private void ApplySafeAreaPosition()
        {
            if (this.rectTransform == null)
                return;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x = this.rectTransform.anchorMin.x;
            anchorMax.x = this.rectTransform.anchorMax.x;

            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;

            this.rectTransform.anchorMin = anchorMin;
            this.rectTransform.anchorMax = anchorMax;
        }
        #endregion
    }
}
