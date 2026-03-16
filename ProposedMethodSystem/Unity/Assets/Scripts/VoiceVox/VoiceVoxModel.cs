namespace VoiceVox
{
    public class VoiceVoxModel
    {
        /// <summary>
        /// APIのレスポンスを受け取る型
        /// </summary>
        [System.Serializable]
        public class RequestModel
        {
            /// <summary>
            /// 
            /// </summary>
            public string text;

            /// <summary>
            /// 
            /// </summary>
            public SpeakerId speaker;
            
            /// <summary>
            /// 
            /// </summary>
            public float speed;
            
            /// <summary>
            /// 
            /// </summary>
            public float pitch;
            
            /// <summary>
            /// 
            /// </summary>
            public float intonation;
            
            /// <summary>
            /// 
            /// </summary>
            public float volume;
        }
    }
}