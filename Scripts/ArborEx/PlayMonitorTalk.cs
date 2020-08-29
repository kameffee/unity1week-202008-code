using System;
using Arbor;
using Cysharp.Threading.Tasks;
using MonitorTalk;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Talk/Monitor/PlayMonitorTalk")]
    public class PlayMonitorTalk : StateBehaviour
    {
        private DoctorMonitorWindow _window;

        [SerializeField]
        [TextArea]
        private string message = default;

        [SerializeField]
        private FlexibleFloat waitTime = new FlexibleFloat(3f);

        [SerializeField]
        private StateLink onNext = new StateLink();

        public override void OnStateAwake()
        {
            _window = FindObjectOfType<DoctorMonitorWindow>();
        }

        public override async void OnStateBegin()
        {
            if (!_window.IsOpen)
                await _window.Open();

            _window.SetMessage(message);
            await _window.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(waitTime.value));

            Transition(onNext);
        }
    }
}