using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

namespace qbot.UI
{
    public class FadeEffect : MonoBehaviour
    {
        /// <summary>
        /// Decide whether to use the class as a singleton pattern.
        /// </summary>
        [Header("[Singleton]")]
        [SerializeField]
        private bool _isSingleton;

        /// <summary>
        /// Deciding whether to make the class a DontDestroyOnLoad GameObject.
        /// </summary>
        [SerializeField]
        private bool _isDontDestroyOnLoad;

        /// <summary>
        /// Fade effect start color.
        /// </summary>
        [Space(5)]
        [Header("[Effect value]")]
        [SerializeField]
        private Color _beginFadeColor = Color.black;

        /// <summary>
        /// Fade effect Startup wait term.
        /// </summary>
        [SerializeField]
        private float _beginWaitTerm = 0.5f;

        /// <summary>
        /// Fade effect's alpha-weighted delay term.
        /// </summary>
        [SerializeField]
        private float _fadeDelayTerm = 0.2f;

        /// <summary>
        /// The alpha weight added to each delay term of the fade effect.
        /// </summary>
        [SerializeField]
        private float _fadeAlphaWeight = 0.2f;

        /// <summary>
        /// The image to which the fade effect will be applied.
        /// </summary>
        [SerializeField]
        private Image _fadeEffectImage;

        public static FadeEffect Instance { get; private set; }

        public Color BeginFadeColor
        {
            private get => _beginFadeColor;
            set => _beginFadeColor = value;
        }

        public float BeginWaitTerm
        {
            private get => _beginWaitTerm;
            set => _beginWaitTerm = value;
        }

        public float FadeDelayTerm
        {
            private get => _fadeDelayTerm;
            set => _fadeDelayTerm = value;
        }

        public float FadeAlphaWeight
        {
            private get => _fadeAlphaWeight;
            set => _fadeAlphaWeight = value;
        }

        public Image FadeEffectImage
        {
            private get => _fadeEffectImage;
            set => _fadeEffectImage = value;
        }

        private void Awake()
        {
            if (_isSingleton)
            {
                if (Instance == null)
                {
                    if (_isDontDestroyOnLoad)
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

        /// <summary>
        /// Starts the fade in or out effect.
        /// </summary>
        /// <param name="isFadeIn">true for fade in, false for fade out</param>
        /// <param name="callback">callback function to be called after the fade effect ends</param>
        public void StartFadeEffect(bool isFadeIn, Action callback = null)
        {
            if (_fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }

            EnableFadeEffectObjects(true);

            _beginFadeColor.a = isFadeIn ? 1.0f : 0.0f;
            _fadeEffectImage.color = _beginFadeColor;

            StartCoroutine(CoroutineFadeEffect(isFadeIn, callback));
        }

        /// <summary>
        /// Disable the fade effect object.
        /// </summary>
        public void DisableFadeEffectObject()
        {
            if (_fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }

            EnableFadeEffectObjects(false);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            DisableFadeEffectObject();
        }

        private IEnumerator CoroutineFadeEffect(bool isFadeIn, Action callback)
        {
            yield return new WaitForSeconds(_beginWaitTerm);

            var fadeColor = _fadeEffectImage.color;
            var fadeAlpha = _beginFadeColor.a;
            var fadeInOutAlphaWeight = isFadeIn ? -1.0f : 1.0f;
            var fadeDelayTermWfs = new WaitForSeconds(_fadeDelayTerm);

            while (fadeAlpha is >= 0.0f and <= 1.0f)
            {
                fadeColor.a = fadeAlpha;
                _fadeEffectImage.color = fadeColor;

                fadeAlpha += _fadeAlphaWeight * fadeInOutAlphaWeight;

                yield return fadeDelayTermWfs;
            }

            fadeColor.a = fadeInOutAlphaWeight > 0 ? 1.0f : 0.0f;
            _fadeEffectImage.color = fadeColor;

            callback?.Invoke();
        }

        private void EnableFadeEffectObjects(bool enable)
        {
            gameObject.SetActive(enable);

            _fadeEffectImage.gameObject.SetActive(enable);
            _fadeEffectImage.enabled = enable;
        }
    }
}