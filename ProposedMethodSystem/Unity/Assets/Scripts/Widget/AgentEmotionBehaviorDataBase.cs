using System.Collections.Generic;
using UnityEngine;

namespace AgentEmotionBehavior
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/AgentEmotionBehaviorDataBase")]
    public class AgentEmotionBehaviorDataBase : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<AgentEmotionBehaviorData> _agentEmotionBehaviorDataList;
        
        /// <summary>
        /// 
        /// </summary>
        public AgentEmotionBehaviorData GetAgentEmotionBehaviorData(AgentEmotionBehaviorType type)
        {
            foreach (var data in _agentEmotionBehaviorDataList)
            {
                if (data.AgentEmotionBehaviorType == type)
                {
                    return data;
                }
            }

            return null;
        }
    }
}