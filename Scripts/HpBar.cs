using DG.Tweening;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Image image;

    [Header("Color settings")]
    [SerializeField]
    private Color normalColor = Color.green;

    [SerializeField]
    private Color warningColor = Color.yellow;

    [SerializeField]
    private Color riskColor = Color.red;

    private float _hp;

    public float MaxValue
    {
        get => slider.maxValue;
        set => slider.maxValue = value;
    }

    public void Play(float toValue)
    {
        _hp = toValue;
        slider.DOValue(toValue, 0.2f)
            .SetEase(Ease.OutBack);
        UpdateColor();
    }

    private void UpdateColor()
    {
        float ratio = _hp / MaxValue;
        if (ratio >= 0.5f)
        {
            image.DOColor(normalColor, 0.2f);
        }
        else if (ratio >= 0.25f)
        {
            image.DOColor(warningColor, 0.2f);
        }
        else
        {
            image.DOColor(riskColor, 0.2f);
        }
    }
}