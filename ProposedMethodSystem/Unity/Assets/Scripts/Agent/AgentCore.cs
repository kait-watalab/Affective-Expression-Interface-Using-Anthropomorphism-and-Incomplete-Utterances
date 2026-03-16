using System;
using Cysharp.Threading.Tasks;
using Dialogue;
using Immersal.XR;
using UniRx;
using UnityEngine;

public class AgentCore : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public IObservable<Unit> OnInitialize => _onInitializeSubject; 
    private Subject<Unit> _onInitializeSubject = new Subject<Unit>();
        
    /// <summary>
    /// 
    /// </summary>
    public TargetDeviceType TargetDeviceType　= TargetDeviceType.plant;

    /// <summary>
    /// 
    /// </summary>
    public int _id;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Localizer _localizer;
    
    [SerializeField] private EyeTrackingDetection _eyeTrackingDetection;
    
    
    [SerializeField] private StartScreenWidget StartScreenWidget;

    private void Start()
    {
        /*
        _localizer
            .OnSuccessfulLocalizations
            .AsObservable()
            .Where(_=>StartScreenWidget.isButtonClicked.Value == true)
            .Where(x =>
            {
                bool match = false;

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] == _id)
                        return true;
                }

                return false;
            })
            .Subscribe(async _ =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(3));
                
                _onInitializeSubject.OnNext(Unit.Default);
                _onInitializeSubject.OnCompleted();
            })
            .AddTo(this.gameObject);
            */

        
        _eyeTrackingDetection
            .OnDeviceDetected
            .Where(x=>x == TargetDeviceType)
            .Subscribe(_ =>
            {
                _onInitializeSubject.OnNext(Unit.Default);
                _onInitializeSubject.OnCompleted();
            })
            .AddTo(this.gameObject);
    }
}
