using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace SceneTransition
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvas;

        public async UniTask Open()
        {
            canvas.alpha = 0;
            await canvas.DOFade(1f, 0.2f)
                .SetUpdate(true);
        }

        public async UniTask Close()
        {
            await canvas.DOFade(0f, 0.2f)
                .SetUpdate(true);
        }
    }
}