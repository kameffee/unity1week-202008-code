using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField]
    private float damage = 1f;

    [SerializeField]
    private GameObject hitParticle;

    private Vector2 lastVelocity;

    private int _hitCount;

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Subscribe(other => OnHit(other));
    }

    private void FixedUpdate()
    {
        lastVelocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void OnHit(Collision2D other)
    {
        Debug.Log("hit: " + other.transform.name);
        if (other.transform.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.OnDamaged(damage);
            GetComponent<Rigidbody2D>().velocity = lastVelocity;
        }
        else if (other.contactCount > 0)
        {
            _hitCount++;
            Vector2 reflect = Vector2.Reflect(lastVelocity, other.contacts[0].normal);
            GetComponent<Rigidbody2D>().velocity = reflect + (other.contacts[0].normal * 0.01f);
        }

        if (hitParticle != null)
            Instantiate(hitParticle, transform.position, Quaternion.identity);

        if (_hitCount > 1)
        {
            Destroy(gameObject);
        }
    }
}