using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Talk
{
    public class TalkWindow : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private RectTransform rectTransform;

        [SerializeField]
        private TextMeshProUGUI message;

        [SerializeField]
        private float perTime = 0.1f;

        [SerializeField]
        private Glyph glyph = default;

        private string _text;

        private Tween _tween;

        public bool IsOpen { get; private set; }

        public void Initialize()
        {
            canvasGroup.alpha = 0;
        }

        public async UniTask Open()
        {
            // オープン時に前回のテキストが残っている場合がある.
            message.text = "";
            
            canvasGroup.alpha = 0;
            rectTransform.DOLocalMoveY(24, 0.2f)
                .SetRelative(true)
                .SetEase(Ease.OutBack);
            await canvasGroup.DOFade(1, 0.2f)
                .SetEase(Ease.Linear);

            canvasGroup.blocksRaycasts = true;
            IsOpen = true;
        }

        public async UniTask Close()
        {
            canvasGroup.blocksRaycasts = false;

            rectTransform.DOLocalMoveY(-24, 0.2f)
                .SetRelative(true)
                .SetEase(Ease.OutSine);
            await canvasGroup.DOFade(0, 0.2f)
                .SetEase(Ease.Linear);

            IsOpen = false;
        }

        public void SetMessage(string message)
        {
            _text = message;
        }

        public async UniTask Play()
        {
            glyph.Invisible();

            message.text = "";
            _tween = message.DOText(_text, _text.Length * perTime)
                .SetEase(Ease.Linear);

            await _tween;
            glyph.Visible();
        }

        public void Complete()
        {
            _tween.Complete();
        }
    }
}