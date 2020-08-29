using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class Energy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float hp = 1;
    
    [SerializeField]
    private SpriteRenderer faceRenderer;

    [SerializeField]
    private GameObject deadEffect;

    [SerializeField]
    private GameObject splitEffect;

    [SerializeField]
    private float knockBackPower = 20;

    [SerializeField]
    private bool autoExpand = true;
    
    private IDisposable _timer;

    private ReactiveProperty<bool> _isAimed = new ReactiveProperty<bool>();

    public IReadOnlyReactiveProperty<bool> IsAimed() => _isAimed;

    public void SetAimed(bool aimed)
    {
        _isAimed.Value = aimed;
    }

    private void Start()
    {
        IsAimed()
            .Subscribe(isAimed =>
            {
                if (isAimed) OnInAimed();
                else OnOutAimed();
            });

        if (autoExpand)
        {
            _timer = Observable.Timer(TimeSpan.FromSeconds(CalcNextTime()))
                .Subscribe(_ => Expand())
                .AddTo(this);
        }
    }

    private void Expand()
    {
        var toScale = transform.localScale * 1.2f;
        if (toScale.x > 3f) toScale = Vector3.one * 2f;
        transform.DOScale(toScale, 0.5f)
            .SetEase(Ease.OutElastic);

        _timer.Dispose();
        if (transform.localScale.x >= 2f)
        {
            return;
        }

        _timer = Observable.Timer(TimeSpan.FromSeconds(CalcNextTime()))
            .Subscribe(_ => Expand())
            .AddTo(this);
    }

    private float CalcNextTime()
    {
        return Mathf.Pow(transform.localScale.x + 1, 3) + Random.Range(0f, 2f);
    }

    /// <summary>
    /// 照準に定められた
    /// </summary>
    public void OnInAimed()
    {
        // faceRenderer.color = Color.red;
    }

    /// <summary>
    /// 照準から外れた
    /// </summary>
    public void OnOutAimed()
    {
        // faceRenderer.color = Color.green;
    }

    void IDamageable.OnDamaged(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (transform.localScale.x / 2 < 0.5f)
            {
                Dead();
            }
            else
            {
                Split();
            }
        }
    }

    private void Dead()
    {
        var effect = Instantiate(deadEffect, transform.position, Quaternion.identity, null);
        effect.transform.localScale = effect.transform.localScale * transform.localScale.x;
        GetComponent<BreakEnergyEmitter>().Emit(Random.Range(1, 4));
        
        Destroy(gameObject);
    }

    public void Split()
    {
        Rigidbody2D[] energys = new Rigidbody2D[2];
        for (int i = 0; i < 2; i++)
        {
            energys[i] = FindObjectOfType<EnergyGenerator>()
                .Generate(transform.localScale.x / 2f, transform.position)
                .GetComponent<Rigidbody2D>();
        }

        foreach (var energy in energys)
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5f;
            energy.AddForceAtPosition(dir * 4, transform.position, ForceMode2D.Force);
        }

        // split effect
        Instantiate(splitEffect, transform.position, Quaternion.identity, null);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            var velocity = other.transform.position - transform.position;
            other.rigidbody.AddForce(velocity.normalized * knockBackPower);
        }
    }

    private void OnDestroy()
    {
        _timer?.Dispose();
    }
}