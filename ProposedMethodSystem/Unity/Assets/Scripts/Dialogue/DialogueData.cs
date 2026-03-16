using System;
using System.Collections.Generic;

namespace Dialogue
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DialogueData
    {
        /// <summary>
        /// 
        /// </summary>
        public AgentUtterancesData AgentUtteranceData;
        
        /// <summary>
        /// 
        /// </summary>
        public List<string> UserExpectedUtterances;
        
        /// <summary>
        /// 
        /// </summary>
        public int Index;
    }
}