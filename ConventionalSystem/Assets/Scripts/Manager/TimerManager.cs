using System;
using UniRx;
using UnityEngine;

/// <summary>
/// 時間を管理する
/// </summary>
public class TimerManager : MonoBehaviour
{
    /// <summary>
    /// 経過時間
    /// </summary>
    public IReactiveProperty<float> OnCurrentTime => _currentTimeProp;
    private FloatReactiveProperty _currentTimeProp = new FloatReactiveProperty(0.0f);
    
    /// <summary>
    /// IDisposable
    /// </summary>
    private IDisposable _disposable;
    
    /// <summary>
    /// 時間を数え始める
    /// </summary>
    public void StartCountTime()
    {
        _disposable = Observable
            .EveryUpdate()
            .Subscribe(_ => _currentTimeProp.Value += Time.deltaTime);
    }

    public void StopCountTime()
    {
        _disposable.Dispose();
    }
}