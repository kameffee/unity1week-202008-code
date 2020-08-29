using System;
using UniRx;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 5;

    public int MaxHp => maxHp;

    [SerializeField]
    private IntReactiveProperty hp = new IntReactiveProperty();

    public IReadOnlyReactiveProperty<int> GetHP() => hp;

    private Subject<int> _onDamage = new Subject<int>();

    public IObservable<int> OnDamage() => _onDamage;

    [SerializeField]
    private PlayerDamageEffect damageEffect;

    [SerializeField]
    private HpBar hpBar;

    private void Awake()
    {
        hp.Value = maxHp;
    }

    private void Start()
    {
        hpBar.MaxValue = MaxHp;
        GetHP()
            .Subscribe(hp => hpBar.Play(hp))
            .AddTo(gameObject);
    }

    public void Damage(int damage)
    {
        hp.Value -= damage;
        _onDamage.OnNext(damage);
    }

    public void SetHp(int value)
    {
        hp.Value = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Energy"))
        {
            if (other.contactCount > 0)
            {
                damageEffect.Emit(other.contacts[0].point);
                Damage(1);
            }
        }
    }
}