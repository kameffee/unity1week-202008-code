using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    public class SceneTransition
    {
        private LoadingDisplay _display;
    
        public bool IsLoading { get; set; } = false;

        public SceneTransition(LoadingDisplay display)
        {
            _display = display;
        }

        public async void Load(string scenePath, Action onUnload = null)
        {
            await LoadAsync(scenePath, onUnload);
        }

        public async UniTask LoadAsync(string scenePath, Action onUnload = null)
        {
            IsLoading = true;
            await _display.Open();

            var activeScene = SceneManager.GetActiveScene();
            await SceneManager.UnloadSceneAsync(activeScene);

            onUnload?.Invoke();

            await UniTask.DelayFrame(5);

            await SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
            var loadedScene = SceneManager.GetSceneByName(scenePath);
            SceneManager.SetActiveScene(loadedScene);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            await _display.Close();

            IsLoading = false;
        }
    }
}