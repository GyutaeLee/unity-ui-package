using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace qbot.UI
{
    public class UIImageAnimation : MonoBehaviour
    {
        [SerializeField] private Image _targetImage;
        [SerializeField] private Sprite[] _animationSprites;
        [SerializeField] private float _animationTerm = 0.1f;
        
        private void Start()
        {
            if (_targetImage == null || _animationSprites.Length == 0)
            {
                Debug.LogError("No target image or animation sprites.");
                Destroy(this);
                return;
            }
            
            StartCoroutine(StartUIImageAnimation());
        }

        private IEnumerator StartUIImageAnimation()
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
                _targetImage.SetNativeSize();
                yield return wfs;
            }
        }
    }
}
