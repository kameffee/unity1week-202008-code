using Arbor;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Platform/Event/CheckPoint")]
    public class CheckPoint : StateBehaviour
    {
        [SerializeField]
        private FlexibleString checkpointName;

        private CheckPointNotification _notification;

        public override void OnStateAwake()
        {
            _notification = FindObjectOfType<CheckPointNotification>();
        }

        public override void OnStateBegin()
        {
            _notification.Play(checkpointName.value);
        }
    }
}