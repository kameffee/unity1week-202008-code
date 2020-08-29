using Arbor;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace ArborEx
{
    public class CinemachineSizeTween : StateBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera camera;

        [SerializeField]
        private Ease ease = Ease.Linear;
    
        [SerializeField]
        private FlexibleFloat duration = new FlexibleFloat(1);

        [SerializeField]
        private FlexibleFloat toValue = new FlexibleFloat(10);

        [SerializeField]
        private StateLink stateLink;
    
        public override async void OnStateBegin()
        {
            await DOTween.To(
                    () => camera.m_Lens.OrthographicSize,
                    value => camera.m_Lens.OrthographicSize = value,
                    toValue.value,
                    duration.value)
                .SetEase(ease);
        

            Transition(stateLink);
        }
    }
}