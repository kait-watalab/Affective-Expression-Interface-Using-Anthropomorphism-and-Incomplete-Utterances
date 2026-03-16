using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class AgentView : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AgentCore _agentCore;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _agentCore
            .OnInitialize
            .Subscribe(_ => _canvasGroup.alpha = 1.0f)
            .AddTo(this.gameObject);
    }
}