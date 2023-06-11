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
            private get
            {
                return progressObject;
            }
            set
            {
                progressObject = value;
            }
        }

        private Dictionary<string, int> _progressKeyDictionary;
        private Dictionary<string, int> ProgressKeyDictionary
        {
            get
            {
                this._progressKeyDictionary ??= new Dictionary<string, int>();
                return this._progressKeyDictionary;
            }
        }
        #endregion

        #region Public functions
        /// <summary>
        /// Open the progress object.
        /// The progresskey must be the same key value when closing the progress object.
        /// </summary>
        /// <param name="progressKey">
        /// The key value that activated the progress object.
        /// The same key value must be entered in the Close() function.
        /// </param>
        public void Open(string progressKey)
        {
            if (this.progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            if (this.progressObject.activeSelf)
                return;

            this.progressObject.SetActive(true);
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
            if (this.progressObject == null)
            {
                Debug.Log("progressObject is null.");
                return;
            }

            if (this.ProgressKeyDictionary.ContainsKey(progressKey) == false)
                return;

            DecreaseProgressKey(progressKey);

            if (this.ProgressKeyDictionary.Count == 0)
            {
                this.progressObject.SetActive(false);
            }
        }
        #endregion

        #region Private functions
        private readonly object lock_object_increaseProgressKey = new();
        private void IncreaseProgressKey(string progressKey)
        {
            lock (lock_object_increaseProgressKey)
            {
                if (this.ProgressKeyDictionary.ContainsKey(progressKey) == false)
                {
                    this.ProgressKeyDictionary.Add(progressKey, 1);
                }
                else
                {
                    this.ProgressKeyDictionary[progressKey] = this.ProgressKeyDictionary[progressKey] + 1;
                }
            }
        }

        private readonly object lock_object_decreaseProgressKey = new();
        private void DecreaseProgressKey(string progressKey)
        {
            lock (lock_object_decreaseProgressKey)
            {
                if (this.ProgressKeyDictionary[progressKey] <= 1)
                {
                    this.ProgressKeyDictionary.Remove(progressKey);
                }
                else
                {
                    this.ProgressKeyDictionary[progressKey] = this.ProgressKeyDictionary[progressKey] - 1;
                }
            }
        }
        #endregion
    }
}