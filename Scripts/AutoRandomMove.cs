using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoRandomMove : MonoBehaviour
{
    private IDisposable _randomMoveTimer;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _randomMoveTimer = Observable.Interval(TimeSpan.FromSeconds(Random.Range(1f, 5f)))
            .Subscribe(_ => RandomMove());
    }

    private void RandomMove()
    {
        _rigidbody2D.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)), ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        _randomMoveTimer?.Dispose();
    }
}