using Arbor;
using Talk;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Talk/PlayTalk")]
    public class PlayTalk : StateBehaviour
    {
        private TalkManager _talk;

        public StateLink OnNext = default;

        [SerializeField]
        [TextArea]
        public string Message;

        public bool isOpen { get; private set; } = false;

        public override void OnStateAwake()
        {
            _talk = FindObjectOfType<TalkManager>();
        }

        public override async void OnStateBegin()
        {
            await _talk.Open(Message);

            isOpen = true;

            await _talk.Play();
        }

        public override void OnStateUpdate()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (_talk.IsPlaying().Value)
                {
                    _talk.Complete();
                }
                else
                {
                    Transition(OnNext);
                }
            }
        }
    }
}