using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Settings
{
    public class SettingsCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private RectTransform window;
    
        public async void Open()
        {
            var duration = 0.2f;
            window.DOAnchorPosY(window.anchoredPosition.y - 24f, duration)
                .SetRelative(true)            
                .SetUpdate(true)
                .SetEase(Ease.OutSine)
                .From();
        
            canvasGroup.alpha = 0;
            await canvasGroup.DOFade(1, duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true);
        }

        public void Close()
        {
            CloseAsync().Forget();
        }

        public async UniTask CloseAsync()
        {
            var duration = 0.2f;
        
            window.DOAnchorPosY(window.anchoredPosition.y - 24f, duration)
                .SetEase(Ease.OutSine)
                .SetRelative(true)
                .SetUpdate(true);
        
            await canvasGroup.DOFade(0, duration)
                .SetEase(Ease.Linear)
                .SetUpdate(true);
            
            Destroy(gameObject);
        }
    }
}