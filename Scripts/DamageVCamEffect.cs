using Cinemachine.PostFX;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// プレイヤーがダメージ受けた時の演出
/// </summary>
public class DamageVCamEffect : MonoBehaviour
{
    [SerializeField]
    private CinemachinePostProcessing vcamera;

    private ChromaticAberration _averration;

    private Vignette _vignette;

    private ColorGrading _colorGrading;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Start()
    {
        vcamera.m_Profile.TryGetSettings(out _averration);
        vcamera.m_Profile.TryGetSettings(out _vignette);
        vcamera.m_Profile.TryGetSettings(out _colorGrading);
        var player = FindObjectOfType<PlayerHealth>();
        player.OnDamage()
            .Subscribe(_ =>
            {
                VignetteShake();
                ChromaticAberrationShake();
                ColorGradingShake();
                PlaySoundEffect();
            })
            .AddTo(player.gameObject);
    }

    public void ChromaticAberrationShake()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(
            DOTween.To(
                    () => _averration.intensity.value,
                    value => _averration.intensity.value = value,
                    1,
                    0.05f)
                .SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f);
        sequence.Append(
            DOTween.To(
                    () => _averration.intensity.value,
                    value => _averration.intensity.value = value,
                    0,
                    1f)
                .SetEase(Ease.InOutSine));
    }

    public void VignetteShake()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(
            DOTween.To(
                    () => _vignette.intensity.value,
                    value => _vignette.intensity.value = value,
                    0.45f,
                    0.05f)
                .SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f);
        sequence.Append(
            DOTween.To(
                    () => _vignette.intensity.value,
                    value => _vignette.intensity.value = value,
                    0,
                    1f)
                .SetEase(Ease.InOutSine));
    }
    
    public void ColorGradingShake()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(
            DOTween.To(
                    () => _colorGrading.saturation.value,
                    value => _colorGrading.saturation.value = value,
                    -50f,
                    0.05f)
                .SetEase(Ease.InOutSine));
        sequence.AppendInterval(1f);
        sequence.Append(
            DOTween.To(
                    () => _colorGrading.saturation.value,
                    value => _colorGrading.saturation.value = value,
                    0,
                    1f)
                .SetEase(Ease.InOutSine));
    }

    /// <summary>
    /// 耳が遠くなったみたいなやつ
    /// </summary>
    public void PlaySoundEffect()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(audioMixer.DOSetFloat("BGM_LowPass", 300, 0.05f));
        sequence.AppendInterval(1f);
        sequence.Append(audioMixer.DOSetFloat("BGM_LowPass", 22000, 1f));
    }
}