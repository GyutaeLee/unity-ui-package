using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace qbot.UI
{
    public class FadeEffect : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// Fade effect start color.
        /// </summary>
        [SerializeField] private Color beginFadeColor = Color.black;
        public Color BeginFadeColor
        {
            private get => beginFadeColor;
            set => beginFadeColor = value;
        }

        /// <summary>
        /// Fade effect Startup wait term.
        /// </summary>
        [SerializeField] private float beginWaitTerm = 0.5f;
        public float BeginWaitTerm
        {
            private get => beginWaitTerm;
            set => beginWaitTerm = value;
        }

        /// <summary>
        /// Fade effect's alpha-weighted delay term.
        /// </summary>
        [SerializeField] private float fadeDelayTerm = 0.2f;
        public float FadeDelayTerm
        {
            private get => fadeDelayTerm;
            set => fadeDelayTerm = value;
        }

        /// <summary>
        /// The alpha weight added to each delay term of the fade effect.
        /// </summary>
        [SerializeField] private float fadeAlphaWeight = 0.2f;
        public float FadeAlphaWeight
        {
            private get => fadeAlphaWeight;
            set => fadeAlphaWeight = value;
        }

        /// <summary>
        /// The image to which the fade effect will be applied.
        /// </summary>
        [SerializeField] private Image fadeEffectImage;
        public Image FadeEffectImage
        {
            private get => fadeEffectImage;
            set => fadeEffectImage = value;
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
                
            gameObject.SetActive(true);

            fadeEffectImage.gameObject.SetActive(true);
            fadeEffectImage.enabled = true;

            beginFadeColor.a = isFadeIn ? 1.0f : 0.0f;
            fadeEffectImage.color = beginFadeColor;

            StartCoroutine(StartFadeEffectAsync(isFadeIn, callback));
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

            gameObject.SetActive(false);

            fadeEffectImage.enabled = false;
            fadeEffectImage.gameObject.SetActive(false);
        }
        #endregion

        #region Private functions
        private IEnumerator StartFadeEffectAsync(bool isFadeIn, Action callback)
        {
            yield return new WaitForSeconds(beginWaitTerm);

            var fadeColor = fadeEffectImage.color;
            var fadeAlpha = beginFadeColor.a;
            var fadeInOutAlphaWeight = isFadeIn == true ? -1.0f : 1.0f;
            var fadeDelayTermWfs = new WaitForSeconds(fadeDelayTerm);

            while (fadeAlpha >= 0.0f && fadeAlpha <= 1.0f)
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
        #endregion
    }

}