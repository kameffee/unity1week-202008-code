using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerDead : MonoBehaviour
{
    private static int kDeadHashKey = Animator.StringToHash("Dead");
    private PlayerHealth health;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        health.GetHP()
            .Where(hp => hp <= 0)
            .Subscribe(_ => { animator.SetBool(kDeadHashKey, true); });

        // 復帰
        health.GetHP()
            .Where(_ => animator.GetBool(kDeadHashKey))
            .Where(hp => hp > 0)
            .Subscribe(_ => { animator.SetBool(kDeadHashKey, false); });
    }
}