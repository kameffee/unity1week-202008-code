using Arbor;

namespace ArborEx
{
    public class IntroClear : StateBehaviour
    {
        public override void OnStateBegin()
        {
            GameManager.Instance.Progress.introComplete = true;
        }
    }
}