using Arbor;
using Arbor.Example;
using UniRx;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("Player/OnPlayerDead")]
    public class OnPlayerDead : StateBehaviour
    {
        private PlayerHealth _health;

        [SerializeField]
        private StateLink onNext;

        public override void OnStateBegin()
        {
            _health = FindObjectOfType<PlayerHealth>();
            _health.GetHP()
                .Where(hp => hp <= 0)
                .Subscribe(_ => Transition(onNext));
        }
    }
}