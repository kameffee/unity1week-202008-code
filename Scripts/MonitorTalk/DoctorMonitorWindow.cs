using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MonitorTalk
{
    public class DoctorMonitorWindow : MonoBehaviour
    {
        [SerializeField]
        private RectTransform doctor;

        [SerializeField]
        private CanvasGroup talkWindow;

        [SerializeField]
        private TextMeshProUGUI message;

        [SerializeField]
        private float perTime = 0.1f;

        private Tween _tween;

        private string _text;

        public bool IsOpen { get; private set; } = false;

        public async UniTask Open()
        {
            message.text = "";
            await doctor.DOAnchorPosX(128f, 0.5f);
            await talkWindow.DOFade(1, 0.2f);
            IsOpen = true;
        }

        public async UniTask Close()
        {
            await talkWindow.DOFade(0, 0.2f);
            await doctor.DOAnchorPosX(-128f, 0.5f);
            IsOpen = false;
        }

        public void SetMessage(string message)
        {
            _text = message;
        }

        public async UniTask Play()
        {
            message.text = "";
            _tween = message.DOText(_text, _text.Length * perTime)
                .SetEase(Ease.Linear);

            await _tween;
        }
    }
}