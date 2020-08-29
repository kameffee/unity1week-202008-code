using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> checkPoints = default;

    public void AddPoint(Vector3 point)
    {
        if (!checkPoints.Contains(point))
        {
            checkPoints.Add(point);
        }
    }

    public Vector2 GetNewestPoint()
    {
        return checkPoints.Last();
    }
}