using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace qbot.UI
{
    public class Progress : MonoBehaviour
    {
#region Properties

        /// <summary>
        /// Objects that will appear when an action is in progress.
        /// </summary>
        [FormerlySerializedAs("progressObject")]
        [SerializeField]
        private GameObject _progressObject;
        public GameObject ProgressObject
        {
            private get => _progressObject;
            set => _progressObject = value;
        }

        private Dictionary<string, int> _progressKeyDictionary;
        private Dictionary<string, int> ProgressKeyDictionary
        {
            get
            {
                _progressKeyDictionary ??= new Dictionary<string, int>();
                return _progressKeyDictionary;
            }
        }

#endregion

#region Fields

        private readonly object _lockObjectIncreaseProgressKey = new();
        private readonly object _lockObjectDecreaseProgressKey = new();

#endregion

#region Public functions

        /// <summary>
        /// Open the progress object.
        /// The progressKey must be the same key value when closing the progress object.
        /// </summary>
        /// <param name="progressKey">
        /// The key value that activated the progress object.
        /// The same key value must be entered in the Close() function.
        /// </param>
        public void Open(string progressKey)
        {
            if (_progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            IncreaseProgressKey(progressKey);

            if (_progressObject.activeSelf)
                return;

            _progressObject.SetActive(true);
        }

        /// <summary>
        /// Close the progress object.
        /// The progress object is closed only when all key values entered in Open() are closed.
        /// </summary>
        /// <param name="progressKey">
        /// The key value that deactivated the progress object.
        /// The same key value must be entered in the Open() function.
        /// </param>
        public void Close(string progressKey)
        {
            if (_progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            if (ProgressKeyDictionary.ContainsKey(progressKey) == false)
                return;

            DecreaseProgressKey(progressKey);

            if (ProgressKeyDictionary.Count == 0)
            {
                _progressObject.SetActive(false);
            }
        }

#endregion

#region Private functions

        private void IncreaseProgressKey(string progressKey)
        {
            lock (_lockObjectIncreaseProgressKey)
            {
                if (ProgressKeyDictionary.TryAdd(progressKey, 1) == false)
                {
                    ProgressKeyDictionary[progressKey] += 1;
                }
            }
        }

        private void DecreaseProgressKey(string progressKey)
        {
            lock (_lockObjectDecreaseProgressKey)
            {
                if (ProgressKeyDictionary[progressKey] <= 1)
                {
                    ProgressKeyDictionary.Remove(progressKey);
                }
                else
                {
                    ProgressKeyDictionary[progressKey] -= 1;
                }
            }
        }

#endregion
    }
}