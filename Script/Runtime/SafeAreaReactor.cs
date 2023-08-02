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
            if (rectTransform == null)
            {
                rectTransform = gameObject.GetComponent<RectTransform>();
            }
        }

        private void ApplySafeAreaPosition()
        {
            if (rectTransform == null)
                return;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x = rectTransform.anchorMin.x;
            anchorMax.x = rectTransform.anchorMax.x;

            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }

#endregion
    }
}