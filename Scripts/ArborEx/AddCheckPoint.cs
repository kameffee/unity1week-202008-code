using Arbor;

namespace ArborEx
{
    public class AddCheckPoint : StateBehaviour
    {
        private CheckPointManager _checkPointManager;
        
        public override void OnStateAwake()
        {
            _checkPointManager = FindObjectOfType<CheckPointManager>();
        }

        public override void OnStateBegin()
        {
            _checkPointManager.AddPoint(transform.position);
        }
    }
}