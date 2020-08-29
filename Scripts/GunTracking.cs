using System;
using Spine.Unity;
using UnityEngine;

public class GunTracking : MonoBehaviour
{
    [SerializeField]
    private string targetName = default;

    [SerializeField]
    private Transform target;

    private SkeletonRenderer _skeleton;

    public void SetTarget(Transform target) => this.target = target;

    private void Awake()
    {
        _skeleton = GetComponent<SkeletonRenderer>();
    }

    private void Update()
    {
        if (target != null)
        {
            var targetBone = _skeleton.skeleton.FindBone(targetName);
            targetBone.SetLocalPosition(transform.InverseTransformPoint(target.position));
        }
    }
}