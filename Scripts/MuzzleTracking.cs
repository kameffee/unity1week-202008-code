using Spine.Unity;
using UnityEngine;

public class MuzzleTracking : MonoBehaviour
{
    [SerializeField]
    private SkeletonRenderer skeleton;

    [SerializeField]
    private string boneName;

    private void Update()
    {
        var way = skeleton.transform.localScale.x > 0 ? 1 : -1;
        var targetBone = skeleton.skeleton.FindBone(boneName);
        transform.position = targetBone.GetWorldPosition(skeleton.transform);
        transform.rotation = targetBone.GetQuaternion();

        // 反転時の補正
        if (way < 0)
        {
            var angles = transform.eulerAngles;
            angles.x += 180;
            angles.z += 180;
            transform.eulerAngles = angles;
        }
    }
}