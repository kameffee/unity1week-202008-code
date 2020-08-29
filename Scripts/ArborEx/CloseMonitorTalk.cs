using System;
using Arbor;
using MonitorTalk;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Talk/Monitor/CloseMonitorTalk")]
    public class CloseMonitorTalk : StateBehaviour
    {
        private MonitorTalkManager _window;

        public override void OnStateAwake()
        {
            _window = FindObjectOfType<MonitorTalkManager>();
        }

        public override async void OnStateBegin()
        {
            await _window.Close();
        }
    }
}