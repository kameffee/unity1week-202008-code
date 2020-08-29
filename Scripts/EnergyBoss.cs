using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnergyBoss : MonoBehaviour
{
    [SerializeField]
    private FloatReactiveProperty brainHp = new FloatReactiveProperty(50);

    [SerializeField]
    private float maxBodyHp = 20;

    [SerializeField]
    private float bodyHp = 20;

    [SerializeField]
    private DamageReceiver brain;

    [SerializeField]
    private DamageReceiver body;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform mouth;
    
    [SerializeField]
    private GameObject energyPrefab;

    [SerializeField]
    private float shotPower = 10;

    [SerializeField]
    private HpBar hpBar;

    [SerializeField]
    private List<Rigidbody2D> colliders;

    [SerializeField]
    private UnityEvent onDead = default;

    private static readonly int kHashKeyStan = Animator.StringToHash("Stan");
    private static readonly int kHashKeyDead = Animator.StringToHash("Dead");
    private static readonly int kHashKeyEyeBlink = Animator.StringToHash("EyeBlink");

    public bool IsStan { get; private set; }

    private void Start()
    {
        body.OnDamage()
            .Where(_ => !IsStan)
            .Subscribe(damage =>
            {
                bodyHp -= damage;
                // Dead判定
                if (bodyHp <= 0)
                {
                    Stan();
                }
            });
        brain.OnDamage()
            .Where(_ => IsStan)
            .Subscribe(damage =>
            {
                brainHp.Value -= damage;
                if (brainHp.Value <= 0)
                {
                    Dead();
                }
            });

        Observable.Interval(TimeSpan.FromSeconds(10))
            .Subscribe(_ =>
            {
                if (Random.Range(0, 100) < 70)
                    AttackLow();
                else
                    AttackHigh();
            })
            .AddTo(gameObject);

        hpBar.MaxValue = brainHp.Value;
        brainHp
            .Subscribe(hp => hpBar.Play(hp))
            .AddTo(gameObject);
    }

    public async void Stan()
    {
        bodyHp = maxBodyHp;
        IsStan = true;
        animator.SetBool(kHashKeyStan, true);
        // タイマー
        await UniTask.Delay(TimeSpan.FromSeconds(6));
        animator.SetBool(kHashKeyStan, false);
        IsStan = false;
    }

    public void Dead()
    {
        Debug.Log("OnDead");
        animator.SetBool(kHashKeyDead, true);
        animator.SetBool(kHashKeyEyeBlink, false);
        SetColliderEnable(false);
        onDead.Invoke();
    }

    public void AttackHigh()
    {
        animator.SetTrigger("Attack_high");
    }

    public void AttackLow()
    {
        animator.SetTrigger("Attack_low");
    }

    public async void atack_gero_small()
    {
        Debug.Log("atack_gero_small");
        SetColliderEnable(false);
        var target = GameObject.FindWithTag("Player");
        var vector = (target.transform.position - mouth.position).normalized;
        var count = Random.Range(1, 2);
        for (int i = 0; i < count; i++)
        {
            var energy= Instantiate(energyPrefab, mouth.position, Quaternion.identity);
            energy.GetComponent<Rigidbody2D>().velocity = vector * shotPower;
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SetColliderEnable(true);
    }

    public async void atack_gero()
    {
        Debug.Log("Attack");
        SetColliderEnable(false);
        var target = GameObject.FindWithTag("Player");
        var vector = (target.transform.position - mouth.position).normalized;
        var count = Random.Range(3, 4);
        for (int i = 0; i < count; i++)
        {
            var energy= Instantiate(energyPrefab, mouth.position, Quaternion.identity);
            energy.GetComponent<Rigidbody2D>().velocity = vector * shotPower;
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        }
        
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SetColliderEnable(true);
    }

    private void SetColliderEnable(bool enable)
    {
        foreach (var collider in colliders)
        {
            collider.simulated = enable;
        }
    }
}