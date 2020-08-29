using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Settings
{
    public class MasterVolumeSlider : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private UnityEvent onPointerUp = default;

        private async void Start()
        {
            await GameManager.Instance.WaitInitialize();
            slider.value = GameManager.Instance.Audio.GetMasterDecibel();
            slider.OnValueChangedAsObservable()
                .Subscribe(value =>
                {
                    GameManager.Instance.Audio.SetMasterDecibel(value);
                });
            slider.OnPointerUpAsObservable()
                .Subscribe(_ =>
                {
                    onPointerUp.Invoke();
                });
        }
    }
}