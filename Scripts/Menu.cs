using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
    }

    public void Open()
    {
        OpenAsync().Forget();
    }
    
    public async UniTask OpenAsync()
    {
        await canvasGroup.DOFade(1, 0.2f)
            .SetUpdate(true);

        canvasGroup.interactable = true;
    }

    public void Close()
    {
        CloseAsync().Forget();
    }

    public async UniTask CloseAsync()
    {
        canvasGroup.interactable = false;

        await canvasGroup.DOFade(1, 0.2f)
            .SetUpdate(true);
    }
}