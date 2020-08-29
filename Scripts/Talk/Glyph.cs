using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Talk
{
    public class Glyph : MonoBehaviour
    {
        [SerializeField]
        private Image glyphImage = default;

        private Tween _tween;

        public void Start()
        {
            glyphImage.DOFade(0, 0);
        }

        public void Visible()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(
                glyphImage.DOFade(1, 0.5f));
            sequence.Join(
                glyphImage.transform.DOLocalMoveY(8, 0.5f)
                    .SetRelative(true));
            sequence.SetLoops(-1, LoopType.Yoyo);
            sequence.Play();
            _tween = sequence;
        }

        public void Invisible()
        {
            _tween?.Kill();
            glyphImage.DOFade(0, 0);
        }
    }
}