using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Talk
{
    public class TalkManager : MonoBehaviour, ITalkStatus
    {
        [SerializeField]
        private GameObject windowPrefab;

        [SerializeField]
        private RectTransform _holder;

        private TalkWindow _cache;

        private BoolReactiveProperty _isPlaying = new BoolReactiveProperty();

        public IReadOnlyReactiveProperty<bool> IsPlaying() => _isPlaying;

        private TalkWindow CacheOrCreate()
        {
            if (_cache != null) return _cache;
            _cache = Instantiate(windowPrefab, _holder).GetComponent<TalkWindow>();
            _cache.Initialize();
            return _cache;
        }

        public async UniTask Open(string message)
        {
            var balloon = CacheOrCreate();
            balloon.SetMessage("");

            if (!balloon.IsOpen)
                await balloon.Open();

            balloon.SetMessage(message);
        }

        public async UniTask Close()
        {
            if (_cache.IsOpen) await _cache.Close();
        }

        public async UniTask Play()
        {
            _isPlaying.Value = true;

            var talkWindow = CacheOrCreate();
            await talkWindow.Play();

            _isPlaying.Value = false;
        }

        public void Complete()
        {
            _cache.Complete();
        }
    }
}