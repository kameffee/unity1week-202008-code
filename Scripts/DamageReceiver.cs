using System;
using UniRx;
using UnityEngine;

public class DamageReceiver : MonoBehaviour, IDamageable
{
    private Subject<float> _onDamage = new Subject<float>();

    public IObservable<float> OnDamage() => _onDamage;

    void IDamageable.OnDamaged(float damage)
    {
        _onDamage.OnNext(damage);
    }
}