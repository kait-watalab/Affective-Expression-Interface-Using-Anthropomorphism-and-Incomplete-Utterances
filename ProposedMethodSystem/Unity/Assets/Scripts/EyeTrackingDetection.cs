using System;
using System.Collections.Generic;
using Dialogue;
using UniRx;
using UnityEngine;

public class EyeTrackingDetection : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public IObservable<TargetDeviceType> OnDeviceDetected => _onDeviceDetectedSubject;
    private Subject<TargetDeviceType> _onDeviceDetectedSubject = new Subject<TargetDeviceType>();

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Camera _camera;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private List<BoxCollider> _colliders;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private float _detectionDistance = 1f;
    
    private void Start()
    {
        Observable
            .EveryFixedUpdate()
            .Subscribe(_ =>
            {
                RaycastHit hit;

                bool isHit = Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                    out hit, _detectionDistance);

                if (isHit)
                {
                    for (int i = 0; i < _colliders.Count; i++)
                    {
                        if (hit.collider.CompareTag(TargetDeviceType.garbage.ToString()))
                        {
                            _onDeviceDetectedSubject.OnNext(TargetDeviceType.garbage);
                            _onDeviceDetectedSubject.OnCompleted();
                        }
                        else if (hit.collider.CompareTag(TargetDeviceType.plant.ToString()))
                        {
                            _onDeviceDetectedSubject.OnNext(TargetDeviceType.plant);
                            _onDeviceDetectedSubject.OnCompleted();
                        }
                    }
                }
            })
            .AddTo(this.gameObject);
    }
}