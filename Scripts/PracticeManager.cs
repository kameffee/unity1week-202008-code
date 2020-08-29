using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PracticeManager: MonoBehaviour
{
    private PlayingStatus _status;
    
    private void Start()
    {
        _status = FindObjectOfType<PlayingStatus>();
        _status.SetPlaying(true);

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ =>
            {
                GameManager.Instance.SceneTransition.Load("Intro", () => Time.timeScale = 1);
            });
    }
}