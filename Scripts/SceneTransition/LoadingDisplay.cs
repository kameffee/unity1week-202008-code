using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    public class LoadingDisplay
    {
        private static readonly string kSceneName = "Loading";
    
        private Scene scene;

        private LoadingScene _loadingScene;

        public async UniTask Open()
        {
            var sceneAsync = SceneManager.LoadSceneAsync(kSceneName, LoadSceneMode.Additive);
            await sceneAsync;

            scene = SceneManager.GetSceneByName("Loading");

            _loadingScene = Object.FindObjectOfType<LoadingScene>();
            await _loadingScene.Open();
        }

        public async UniTask Close()
        {
            if (_loadingScene != null)
            {
                await _loadingScene.Close();
            }

            await SceneManager.UnloadSceneAsync(scene);
        }
    }
}