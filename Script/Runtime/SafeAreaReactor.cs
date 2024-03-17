using UnityEngine;

namespace qbot.UI
{
    /// <summary>
    /// Add the SafeAreaReactor class as a Component to an object that needs SafeArea processing,
    /// it moves the object's anchor by the SafeArea range.
    /// </summary>
    public class SafeAreaReactor : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private void Start()
        {
            PrepareBaseObjects();
            ApplySafeAreaPosition();
        }

        private void PrepareBaseObjects()
        {
            if (_rectTransform == null)
            {
                _rectTransform = gameObject.GetComponent<RectTransform>();
            }
        }

        private void ApplySafeAreaPosition()
        {
            if (_rectTransform == null)
                return;

            var safeArea = Screen.safeArea;
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;

            anchorMin.x = _rectTransform.anchorMin.x;
            anchorMax.x = _rectTransform.anchorMax.x;

            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;

            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}