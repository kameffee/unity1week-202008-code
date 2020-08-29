using Arbor;
using UnityEngine;

namespace ArborEx
{
    [AddComponentMenu("")]
    [AddBehaviourMenu("SceneTransition")]
    public class SceneTransition : StateBehaviour
    {
        [SerializeField]
        private FlexibleString sceneName = new FlexibleString();

        public override void OnStateBegin()
        {
            GameManager.Instance.SceneTransition.Load(sceneName.value);
        }
    }
}