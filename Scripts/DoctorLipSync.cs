using System;
using Talk;
using UnityEngine;
using UniRx;

public class DoctorLipSync : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    
    private ITalkStatus _talkStatus;

    private int LipSyncKey = Animator.StringToHash("LipSync");
    
    private void Start()
    {
        _talkStatus = GameObjectExtensions.FindObjectOfInterface<ITalkStatus>();
        _talkStatus.IsPlaying()
            .Subscribe(value =>
            {
                _animator.SetBool(LipSyncKey, value);
            });
    }
    
    

}