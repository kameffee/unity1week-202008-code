using UniRx;
using UnityEngine;

public class PlayingStatus : MonoBehaviour
{
    [SerializeField]
    private BoolReactiveProperty isPlaying = new BoolReactiveProperty(false);

    public IReadOnlyReactiveProperty<bool> IsPlaying() => isPlaying;

    public void SetPlaying(bool playing) => isPlaying.Value = playing;
}