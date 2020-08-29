using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private string targetName = "target";

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (target != null)
        {
            var targetBone = GetComponent<SkeletonRenderer>().skeleton.FindBone(targetName);
            targetBone.SetLocalPosition(transform.InverseTransformPoint(target.position));
        }
    }
}
