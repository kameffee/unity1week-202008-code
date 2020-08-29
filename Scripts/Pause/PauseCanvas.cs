using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Pause
{
    public class PauseCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        private Subject<Unit> _onClose = new Subject<Unit>();

        public IObservable<Unit> OnClose() => _onClose;

        public async UniTask Open()
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1, 0.2f)
                .SetUpdate(true);
        }

        public async UniTask Close()
        {
            canvasGroup.DOFade(0, 0.2f)
                .SetUpdate(true);
            _onClose.OnNext(Unit.Default);
            Destroy(gameObject);
        }

        public void Resume()
        {
            Close();
        }
    }
}