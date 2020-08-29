using System;
using System.Collections.Generic;
using Pause;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Sight sight;

    [SerializeField]
    private IntReactiveProperty currentIndex = new IntReactiveProperty(0);
    
    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private List<Weapon> weapons = new List<Weapon>();

    private PauseManager _pauseManager;

    private PlayingStatus _status;

    private void Start()
    {
        _status = FindObjectOfType<PlayingStatus>();
        _pauseManager = FindObjectOfType<PauseManager>();
        
        sight.enabled = false;
        this.UpdateAsObservable()
            .Where(_ => !_pauseManager.isPause)
            .Where(_ => _status.IsPlaying().Value)
            .Where(_ => Input.GetMouseButton(0))
            .ThrottleFirst(TimeSpan.FromSeconds(weapon.Interval))
            .Subscribe(_ =>
            {
                Fire();
            })
            .AddTo(gameObject);

        currentIndex
            .Subscribe(index =>
            {
                weapon = weapons[index];
            });

        this.UpdateAsObservable()
            .Where(_ => _status.IsPlaying().Value)
            .Where(_ => Input.GetKeyDown(KeyCode.E))
            .Subscribe(_ =>
            {
                if (currentIndex.Value + 1 <= weapons.Count)
                {
                    currentIndex.Value++;
                }
                else
                {
                    currentIndex.Value = 0;
                }
            });
    }

    public void Fire()
    {
        if (weapon.Available())
        {
            weapon.Fire();
        }
    }
}