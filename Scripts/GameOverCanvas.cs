using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvas;
    
    [SerializeField]
    private TextMeshProUGUI title;

    [SerializeField]
    private Button continueButton;

    private void Start()
    {
        title.color = new Color(1, 1, 1, 0);
    }

    public async void Open()
    {
        await title.DOFade(1, 1f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }

    public async UniTask Close()
    {
        await canvas.DOFade(0, 0.5f);
    }

    public async void Continue()
    {
        await Close();
        
        // TODO: continue process
        var stageManager = FindObjectOfType<StageManager>();
        stageManager.Continue();

        Destroy(gameObject);
    }
}