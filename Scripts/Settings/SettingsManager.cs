using Arbor;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        private static SettingsManager _instance;

        public static SettingsManager Instance
        {
            get
            {
                if (_instance == null) _instance = Create();
                return _instance;
            }
        }

        private static SettingsManager Create()
        {
            var asset = Resources.Load<SettingsManager>("SettingsManager");
            var manager = Instantiate(asset);
            DontDestroyOnLoad(manager);
            return manager;
        }

        [SerializeField]
        private SettingsCanvas settingsCanvas;

        private SettingsCanvas _cache;

        public bool IsOpen() => _cache != null;

        public void Open()
        {
            _cache = Instantiate(settingsCanvas);
            _cache.Open();
        }

        public void Close()
        {
            _cache.Close();
            _cache = null;
        }
    }
}