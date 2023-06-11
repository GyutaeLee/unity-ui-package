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
            private get
            {
                return beginFadeColor;
            }
            set
            {
                beginFadeColor = value;
            }
        }

        /// <summary>
        /// Fade effect Startup wait term.
        /// </summary>
        [SerializeField] private float beginWaitTerm = 0.5f;
        public float BeginWaitTerm
        {
            private get
            {
                return beginWaitTerm;
            }
            set
            {
                beginWaitTerm = value;
            }
        }

        /// <summary>
        /// Fade effect's alpha-weighted delay term.
        /// </summary>
        [SerializeField] private float fadeDelayTerm = 0.2f;
        public float FadeDelayTerm
        {
            private get
            {
                return fadeDelayTerm;
            }
            set
            {
                fadeDelayTerm = value;
            }
        }

        /// <summary>
        /// The alpha weight added to each delay term of the fade effect.
        /// </summary>
        [SerializeField] private float fadeAlphaWeight = 0.2f;
        public float FadeAlphaWeight
        {
            private get
            {
                return fadeAlphaWeight;
            }
            set
            {
                fadeAlphaWeight = value;
            }
        }

        /// <summary>
        /// The image to which the fade effect will be applied.
        /// </summary>
        [SerializeField] private Image fadeEffectImage;
        public Image FadeEffectImage
        {
            private get
            {
                return fadeEffectImage;
            }
            set
            {
                fadeEffectImage = value;
            }
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
            if (this.fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }
                
            this.gameObject.SetActive(true);

            this.fadeEffectImage.gameObject.SetActive(true);
            this.fadeEffectImage.enabled = true;

            this.beginFadeColor.a = isFadeIn == true ? 1.0f : 0.0f;
            this.fadeEffectImage.color = this.beginFadeColor;

            StartCoroutine(StartFadeEffectAsync(isFadeIn, callback));
        }

        /// <summary>
        /// Disable the fade effect object.
        /// </summary>
        public void DisableFadeEffectObject()
        {
            if (this.fadeEffectImage == null)
            {
                Debug.Log("fadeEffectImage is null.");
                return;
            }

            this.gameObject.SetActive(false);

            this.fadeEffectImage.enabled = false;
            this.fadeEffectImage.gameObject.SetActive(false);
        }
        #endregion

        #region Private functions
        private IEnumerator StartFadeEffectAsync(bool isFadeIn, Action callback)
        {
            yield return new WaitForSeconds(this.beginWaitTerm);

            var fadeColor = this.fadeEffectImage.color;
            var fadeAlpha = this.beginFadeColor.a;
            var fadeInOutAlphaWeight = isFadeIn == true ? -1.0f : 1.0f;
            var fadeDelayTermWFS = new WaitForSeconds(this.fadeDelayTerm);

            while (fadeAlpha >= 0.0f && fadeAlpha <= 1.0f)
            {
                fadeColor.a = fadeAlpha;
                this.fadeEffectImage.color = fadeColor;

                fadeAlpha += this.fadeAlphaWeight * fadeInOutAlphaWeight;

                yield return fadeDelayTermWFS;
            }

            if (fadeInOutAlphaWeight > 0)
            {
                fadeColor.a = 1.0f;
            }
            else
            {
                fadeColor.a = 0.0f;
            }

            this.fadeEffectImage.color = fadeColor;

            callback?.Invoke();
        }
        #endregion
    }

}