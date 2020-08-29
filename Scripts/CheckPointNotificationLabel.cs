using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CheckPointNotificationLabel : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    [SerializeField]
    private TextMeshProUGUI text;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = transform as RectTransform;
        canvasGroup.alpha = 0;
    }

    public async UniTask Play(string message)
    {
        text.text = message;
        text.rectTransform.anchoredPosition = new Vector2(1000, text.rectTransform.anchoredPosition.y);
        _rectTransform.localScale = new Vector2(_rectTransform.localScale.x, 0f);

        var sequence = DOTween.Sequence();
        sequence.Append(
            canvasGroup.DOFade(1, 0.2f));
        sequence.Join(
            _rectTransform.DOScaleY(1, 0.2f));
        
        sequence.Append(
            text.rectTransform.DOAnchorPos(Vector2.zero + new Vector2(24, 0), 0.5f));
        sequence.Append(
            text.rectTransform.DOAnchorPos(Vector2.zero + new Vector2(-24, 0), 3f));
        sequence.Append(
            text.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.5f));
        sequence.Append(
            canvasGroup.DOFade(0, 0.2f));
        sequence.Join(
            _rectTransform.DOScaleY(0, 0.2f));

        await sequence.Play();
    }
}