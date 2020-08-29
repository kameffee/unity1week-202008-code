using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class JellySpawnHole : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float hp = 10;

    [SerializeField]
    private GameObject deadEffect;
    
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private int maxCount = 5;

    [SerializeField]
    private float generateCheckIntervalTime = 2f;

    private int _count;

    private void Start()
    {
        for (int i = 0; i < maxCount; i++)
        {
            Spawn();
        }

        Observable.Interval(TimeSpan.FromSeconds(generateCheckIntervalTime))
            .Where(_ => _count < maxCount)
            .Subscribe(_ => Spawn())
            .AddTo(gameObject);
    }

    public void Spawn()
    {
        var energy = Instantiate(prefab, transform.position, Quaternion.identity);
        energy.OnDestroyAsObservable()
            .Subscribe(_ =>
            {
                _count--;
            });
 
        _count++;
    }

    public void OnDamaged(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}