using TMPro;
using UnityEngine;

namespace AgentEmotionBehavior
{
    [System.Serializable]
    public class AgentEmotionBehaviorData
    {
        /// <summary>
        /// 
        /// </summary>
        public AgentEmotionBehaviorType AgentEmotionBehaviorType;

        /// <summary>
        /// 
        /// </summary>
        public Sprite FacialExpressionSprite;

        /// <summary>
        /// 
        /// </summary>
        public Sprite SpeechBubbleSprite;

        /// <summary>
        /// 
        /// </summary>
        public Color UtterancTextColor;

        /// <summary>
        /// 
        /// </summary>
        public TMP_Asset UtterancTextFont;
    }
}