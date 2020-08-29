using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 1f;
    
    [SerializeField]
    private GameObject hitParticle;

    [SerializeField]
    private float lifeTime = 5;
    
    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Subscribe(other => OnHit(other));

        Observable.Timer(TimeSpan.FromSeconds(lifeTime))
            .Subscribe(_ => Destroy(gameObject))
            .AddTo(gameObject);
    }

    private void OnHit(Collision2D other)
    {
        if (other.transform.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.OnDamaged(damage);
        }
        
        if (hitParticle != null)
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
    }
}