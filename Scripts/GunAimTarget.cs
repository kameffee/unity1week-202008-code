using System;
using UnityEngine;

public class GunAimTarget : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        var screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(_camera.transform.position.z);
        transform.position = _camera.ScreenToWorldPoint(screenPos);
    }
}