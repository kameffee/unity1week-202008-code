using Arbor;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("IsIntroClear")]
    [BehaviourTitle("IsIntroClear")]
    public class IsIntroClear : Calculator
    {
        [SerializeField]
        private OutputSlotBool output;

        public override void OnCalculate()
        {
            output.SetValue(GameManager.Instance.Progress.introComplete);
        }
    }
}