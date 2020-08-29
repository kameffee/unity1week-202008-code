using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace MonitorTalk
{
    public class MonitorTalkManager : MonoBehaviour, ITalkStatus
    {
        private DoctorMonitorWindow _window;

        private BoolReactiveProperty _isPlaying = new BoolReactiveProperty();

        public IReadOnlyReactiveProperty<bool> IsPlaying() => _isPlaying;

        private void Awake()
        {
            _window = FindObjectOfType<DoctorMonitorWindow>();
        }

        public async UniTask Open(string message)
        {
            if (!_window.IsOpen)
            {
                await _window.Open();
            }

            _window.SetMessage(message);
        }

        public async UniTask Close()
        {
            if (_window.IsOpen)
                await _window.Close();
        }

        public async UniTask Play()
        {
            _isPlaying.Value = true;
            
            await _window.Play();

            _isPlaying.Value = false;
        }
    }
}