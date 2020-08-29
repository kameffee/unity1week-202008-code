using UniRx;

public interface ITalkStatus
{
    IReadOnlyReactiveProperty<bool> IsPlaying();
}