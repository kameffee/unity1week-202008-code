using System;
using UnityEngine;

public class EyeTrackingArea : MonoBehaviour
{
    [SerializeField]
    private EyeTracking eyeTracking;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eyeTracking.SetTarget(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eyeTracking.SetTarget(null);
        }
    }
}