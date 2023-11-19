using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace qbot.UI
{
    public class FadeEffect : MonoBehaviour
    {
#region Fields

        /// <summary>
        /// Decide whether to use the class as a singleton pattern.
        /// </summary>
        [Header("[Singleton]")]
        [SerializeField]
        private bool isSingleton;

        /// <summary>
        /// Deciding whether to make the class a DontDestroyOnLoad GameObject.
        /// </summary>
        [SerializeField]
        private bool isDontDestroyOnLoad;

        /// <summary>
        /// Fade effect start color.
        /// </summary>
        [Space(5)]
        [Header("[Effect value]")]
        [SerializeField]
        private Color beginFadeColor = Color.black;

        /// <summary>
        /// Fade effect Startup wait term.
        /// </summary>
        [SerializeField]
        private float beginWaitTerm = 0.5f;

        /// <summary>
        /// Fade effect's alpha-weighted delay term.
        /// </summary>
        [SerializeField]
        private float fadeDelayTerm = 0.2f;

        /// <summary>
        /// The alpha weight added to each delay term of the fade effect.
        /// </summary>
        [SerializeField]
        private float fadeAlphaWeight = 0.2f;

        /// <summary>
        /// The image to which the fade effect will be applied.
        /// </summary>
        [SerializeField]
        private Image fadeEffectImage;

#endregion

#region Properties

        public static FadeEffect Instance { get; private set; }

        public Color BeginFadeColor
        {
            private get => beginFadeColor;
            set => beginFadeColor = value;
        }

        public float BeginWaitTerm
        {
            private get => beginWaitTerm;
            set => beginWaitTerm = value;
        }

        public float FadeDelayTerm
        {
            private get => fadeDelayTerm;
            set => fadeDelayTerm = value;
        }

        public float FadeAlphaWeight
        {
            private get => fadeAlphaWeight;
            set => fadeAlphaWeight = value;
        }

        public Image FadeEffectImage
        {
            private get => fadeEffectImage;
            set => fadeEffectImage = value;
        }

#endregion

#region Monobehaviour functions

        private void Awake()
        {
            if (isSingleton)
            {
                if (Instance == null)
                {
                    if (isDontDestroyOnLoad)
                    {
                        DontDestroyOnLoad(gameObject);
                    }

                    Instance = this;
                }
                else
                {
                    Destroy(this);
                }
            }
        }

        private void Start()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

#endregion

#region Public functions

        /// <summary>
        /// Starts the fade in or out effect.
        /// </summary>
        /// <param name="isFadeIn">true for fade in, false for fade out</param>
        /// <param name="callback">callback function to be called after the fade effect ends</param>
        public void StartFadeEffect(bool isFadeIn, Action callback = null)
        {
            if (fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }

            EnableFadeEffectObjects(true);

            beginFadeColor.a = isFadeIn ? 1.0f : 0.0f;
            fadeEffectImage.color = beginFadeColor;

            StartCoroutine(CoroutineFadeEffect(isFadeIn, callback));
        }

        /// <summary>
        /// Disable the fade effect object.
        /// </summary>
        public void DisableFadeEffectObject()
        {
            if (fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }

            EnableFadeEffectObjects(false);
        }

#endregion

#region Private functions

        private void OnSceneUnloaded(Scene scene)
        {
            DisableFadeEffectObject();
        }

        private IEnumerator CoroutineFadeEffect(bool isFadeIn, Action callback)
        {
            yield return new WaitForSeconds(beginWaitTerm);

            var fadeColor = fadeEffectImage.color;
            var fadeAlpha = beginFadeColor.a;
            var fadeInOutAlphaWeight = isFadeIn ? -1.0f : 1.0f;
            var fadeDelayTermWfs = new WaitForSeconds(fadeDelayTerm);

            while (fadeAlpha is >= 0.0f and <= 1.0f)
            {
                fadeColor.a = fadeAlpha;
                fadeEffectImage.color = fadeColor;

                fadeAlpha += fadeAlphaWeight * fadeInOutAlphaWeight;

                yield return fadeDelayTermWfs;
            }

            fadeColor.a = fadeInOutAlphaWeight > 0 ? 1.0f : 0.0f;
            fadeEffectImage.color = fadeColor;

            callback?.Invoke();
        }

        private void EnableFadeEffectObjects(bool enable)
        {
            gameObject.SetActive(enable);

            fadeEffectImage.gameObject.SetActive(enable);
            fadeEffectImage.enabled = enable;
        }

#endregion
    }
}