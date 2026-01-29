using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace qbot.UI
{
    public class ImageAnimation : MonoBehaviour
    {
        [SerializeField] private bool _isAutoStart;
        
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite[] _animationSprites;
        [SerializeField] private float _animationTerm = 0.1f;
        [SerializeField] private bool _setNativeSizeEachFrame = true;

        private Coroutine _runningCoroutine;

        private void OnEnable()
        {
            if (_isAutoStart)
            {
                if (_targetImage == null || _animationSprites.Length == 0 || _runningCoroutine != null)
                    return;

                _runningCoroutine = StartCoroutine(CoroutineStartAnimation());
            }
        }

        private void OnDisable()
        {
            StopAnimation();
        }

        public void ChangeAnimationSprites(Sprite[] sprites)
        {
            _animationSprites = sprites;
            _targetImage.sprite = _animationSprites[0];
        }

        public void StartAnimation()
        {
            if (_runningCoroutine != null)
                return;
            
            _runningCoroutine = StartCoroutine(CoroutineStartAnimation());
        }

        public void StopAnimation()
        {
            if (_runningCoroutine == null) 
                return;
            
            StopCoroutine(_runningCoroutine);
            _runningCoroutine = null;
        }

        private IEnumerator CoroutineStartAnimation()
        {
            var wfs = new WaitForSeconds(_animationTerm);

            var index = 0;
            while (true)
            {
                if (index >= _animationSprites.Length)
                {
                    index = 0;
                }

                _targetImage.sprite = _animationSprites[index++];

                if (_setNativeSizeEachFrame)
                {
                    _targetImage.SetNativeSize();
                }

                yield return wfs;
            }
        }
    }
}