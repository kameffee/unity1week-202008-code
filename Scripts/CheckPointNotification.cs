using UnityEngine;

public class CheckPointNotification : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    private CheckPointNotificationLabel _cache;

    public async void Play(string message)
    {
        _cache = Instantiate(prefab).GetComponentInChildren<CheckPointNotificationLabel>();
        await _cache.Play(message);
        Destroy(_cache);
    }
}