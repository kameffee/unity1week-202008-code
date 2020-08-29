using System;
using Audio;
using Cysharp.Threading.Tasks;
using SceneTransition;
using Settings;
using UniRx;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject(nameof(GameManager));
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

    public bool IsInitialize { get; private set; }

    public bool Initializing { get; private set; }

    public SceneTransition.SceneTransition SceneTransition { get; private set; }

    public AudioManager Audio { get; private set; }

    public SettingsManager Settingses { get; private set; }

    public GameProgress Progress { get; private set; }

    public async UniTask WaitInitialize() => await UniTask.WaitUntil(() => IsInitialize);

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoad()
    {
        Instance.Initialize().Forget();
    }

    public async UniTask Initialize()
    {
        if (IsInitialize) return;
        if (Initializing) return;

        Progress = new GameProgress();

        SceneTransition = new SceneTransition.SceneTransition(new LoadingDisplay());

        Audio = AudioManager.Instance;
        Settingses = SettingsManager.Instance;

        IsInitialize = true;
    }
}