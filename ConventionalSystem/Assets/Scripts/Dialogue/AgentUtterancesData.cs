using System;
using AgentEmotionBehavior;

namespace Dialogue
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AgentUtterancesData{
        /// <summary>
        /// 
        /// </summary>
        public string AgentUtterance;
        
        /// <summary>
        /// 
        /// </summary>
        public AgentEmotionBehaviorType AgentEmotionBehaviorType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentUtterance"></param>
        /// <param name="agentEmotionBehaviorType"></param>
        public AgentUtterancesData(string agentUtterance, AgentEmotionBehaviorType agentEmotionBehaviorType)
        {
            AgentUtterance = agentUtterance;
            AgentEmotionBehaviorType = agentEmotionBehaviorType;
        }
    }
}