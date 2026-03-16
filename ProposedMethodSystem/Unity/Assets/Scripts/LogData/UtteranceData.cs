namespace Commons.LogData
{
    [System.Serializable]
    public class UtteranceData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Utterance;

        /// <summary>
        /// 
        /// </summary>
        public float SectionTime;

        /// <summary>
        /// 
        /// </summary>
        public UtteranceData(string utterance, float sectionTime)
        {
            Utterance = utterance;
            SectionTime = sectionTime;
        }
    }
}