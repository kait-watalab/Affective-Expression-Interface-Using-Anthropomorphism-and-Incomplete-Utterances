using System;
using System.Collections.Generic;
using UniRx;
using Dialogue;
using UnityEngine;

public class AgentProvider : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] List<AgentCore> _agentCores = new List<AgentCore>();

    /// <summary>
    /// 最初に初期化が完了した AgentCore を返す
    /// </summary>
    public IObservable<Unit> GetAgent(TargetDeviceType targetDeviceType)
    {
        AgentCore agent = null;
        
        foreach (var agentCore in _agentCores)
        {
            if (agentCore.TargetDeviceType != targetDeviceType)
            {
                agentCore.gameObject.SetActive(false);
            }
            else
            {
                agent = agentCore;
            }
        }

        return agent.OnInitialize;
    }
}
