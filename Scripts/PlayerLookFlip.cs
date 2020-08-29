using System;
using Cinemachine;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerLookFlip : MonoBehaviour
{
    [SerializeField]
    private SkeletonRenderer renderer;

    [SerializeField]
    private Transform target;

    private PlayingStatus _status;

    private void Awake()
    {
        _status = FindObjectOfType<PlayingStatus>();
    }

    private void Update()
    {
        if (_status.IsPlaying().Value)
        {
            // Targetが自機より左側にある
            var vector = target.position - transform.position;
            if (vector.x < 0)
            {
                var toScale = renderer.transform.localScale;
                toScale.x = Mathf.Abs(toScale.x) * -1;
                renderer.transform.localScale = toScale;
            }
            else
            {
                var toScale = renderer.transform.localScale;
                toScale.x = Mathf.Abs(toScale.x);
                renderer.transform.localScale = toScale;
            }
        }
    }
}