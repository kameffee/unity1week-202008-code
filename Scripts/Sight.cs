using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pause;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

/// <summary>
/// 発射口
/// </summary>
public class Sight : MonoBehaviour
{
    [SerializeField]
    private LineRenderer sightRenderer;

    [SerializeField]
    private LayerMask beamHitLayerMask = default;

    private Camera _camera;

    private List<Vector3> _hitList = new List<Vector3>();

    private Subject<Vector3> _onFire = new Subject<Vector3>();

    public IObservable<Vector3> OnFire() => _onFire;

    private RaycastHit2D[] hits = new RaycastHit2D[12];

    private List<RaycastHit2D> _hitEnergys = new List<RaycastHit2D>(128);

    public IReadOnlyList<RaycastHit2D> GetHitEnergyList() => _hitEnergys;

    private PauseManager _pauseManager;

    private void Start()
    {
        _camera = Camera.main;
        _pauseManager = FindObjectOfType<PauseManager>();

        this.UpdateAsObservable()
            .Where(_ => !_pauseManager.isPause)
            .Subscribe(_ =>
            {
                var screenPos = Input.mousePosition;
                screenPos.z = Mathf.Abs(_camera.transform.position.z);

                // 予測ライン
                _hitList.Clear();

                // Aimed off
                foreach (var energy in _hitEnergys
                    .Where(hit2D => hit2D.collider != null)
                    .Select(hit2D => hit2D.transform.GetComponent<Energy>()))
                {
                    energy?.SetAimed(false);
                }

                _hitEnergys.Clear();

                LayerMask layerMask = beamHitLayerMask;
                var worldPoint = _camera.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dir = worldPoint - transform.position;
                Vector2 origin = transform.position;
                _hitList.Add(origin);

                int reflectCount = 3;

                for (int i = 0; i < reflectCount; i++)
                {
                    var count = Physics2D.RaycastNonAlloc(origin, dir, hits, Mathf.Infinity, layerMask);
                    for (var index = 0; index < count; index++)
                    {
                        var hit = hits[index];
                        if (hit.collider != null)
                        {
                            if (hit.transform.CompareTag("Energy"))
                            {
                                if (hit.transform.TryGetComponent<Energy>(out var energy))
                                {
                                    energy.SetAimed(true);
                                }

                                // 重複チェックしてないか
                                if (!_hitEnergys.Contains(hit))
                                    _hitEnergys.Add(hit);
                            }

                            if (hit.transform.CompareTag("Reflect"))
                            {
                                _hitList.Add(hit.point);
                                origin = hit.point + (hit.normal * 0.01f);
                                dir = Vector2.Reflect(dir, hit.normal);
                            }
                        }
                    }
                }

                sightRenderer.positionCount = _hitList.Count;
                sightRenderer.SetPositions(_hitList.ToArray());
            })
            .AddTo(gameObject);
    }

    private void OnEnable()
    {
        sightRenderer.enabled = true;
    }

    private void OnDisable()
    {
        sightRenderer.enabled = false;
    }

    public void Fire()
    {
        var screenPos = Input.mousePosition;
        screenPos.z = Mathf.Abs(_camera.transform.position.z);
        var worldPoint = _camera.ScreenToWorldPoint(screenPos);

        var dir = worldPoint - transform.position;
        _onFire.OnNext(dir);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            for (var index = 1; index < _hitList.Count; index++)
            {
                var prevPoint = _hitList[index - 1];
                var point = _hitList[index];
                Gizmos.DrawSphere(point, 0.1f);
                Gizmos.DrawLine(prevPoint, point);
            }
        }
    }
}