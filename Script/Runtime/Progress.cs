using System.Collections.Generic;
using UnityEngine;

namespace qbot.UI
{
    public class Progress : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Objects that will appear when an action is in progress.
        /// </summary>
        [SerializeField] private GameObject progressObject;
        public GameObject ProgressObject
        {
            private get => progressObject;
            set => progressObject = value;
        }

        private Dictionary<string, int> progressKeyDictionary;
        private Dictionary<string, int> ProgressKeyDictionary
        {
            get
            {
                progressKeyDictionary ??= new Dictionary<string, int>();
                return progressKeyDictionary;
            }
        }
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
            IncreaseProgressKey(progressKey);
            
            if (progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            if (progressObject.activeSelf)
                return;

            progressObject.SetActive(true);
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
            if (progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            if (ProgressKeyDictionary.ContainsKey(progressKey) == false)
                return;

            DecreaseProgressKey(progressKey);

            if (ProgressKeyDictionary.Count == 0)
            {
                progressObject.SetActive(false);
            }
        }
        #endregion

        #region Private functions
        private readonly object lockObjectIncreaseProgressKey = new();
        private void IncreaseProgressKey(string progressKey)
        {
            lock (lockObjectIncreaseProgressKey)
            {
                if (ProgressKeyDictionary.ContainsKey(progressKey) == false)
                {
                    ProgressKeyDictionary.Add(progressKey, 1);
                }
                else
                {
                    ProgressKeyDictionary[progressKey] += 1;
                }
            }
        }

        private readonly object lockObjectDecreaseProgressKey = new();
        private void DecreaseProgressKey(string progressKey)
        {
            lock (lockObjectDecreaseProgressKey)
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