using System.Collections.Generic;

namespace Commons.LogData
{
    /// <summary>
    /// セーブデータを管理する
    /// </summary>
    [System.Serializable]
    public class LogData
    {
        /// <summary>
        /// 
        /// </summary>
        public string TargetDeviceType;
        
        /// <summary>
        /// 
        /// </summary>
        public float TotalTime;
        
        /// <summary>
        /// 
        /// </summary>
        public List<UtteranceData> UtterancesData;

        /// <summary>
        /// 
        /// </summary>
        public LogData()
        {
            TargetDeviceType　= string.Empty;
            TotalTime = 0.0f;
            UtterancesData = new List<UtteranceData>();
        }
    }
}
