using Arbor;
using Talk;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Talk/CloseTalk")]
    public class CloseTalk : StateBehaviour
    {
        private TalkManager _talk;

        public StateLink OnNext = default;

        public override void OnStateAwake()
        {
            _talk = FindObjectOfType<TalkManager>();
        }

        public override async void OnStateBegin()
        {
            await _talk.Close();

            Transition(OnNext);
        }
    }
}