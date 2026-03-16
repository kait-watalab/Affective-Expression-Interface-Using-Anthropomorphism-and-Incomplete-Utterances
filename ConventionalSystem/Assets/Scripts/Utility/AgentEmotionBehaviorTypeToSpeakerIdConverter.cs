using VoiceVox;
using AgentEmotionBehavior;

namespace Utility
{
    public static class AgentEmotionBehaviorTypeToSpeakerIdConverter
    {
        public static SpeakerId Convert(AgentEmotionBehavior.AgentEmotionBehaviorType type)
        {
            SpeakerId result;
            
            switch (type)
            {
                case AgentEmotionBehaviorType.Neutral:
                    result = SpeakerId.WHITECUL_NORMAL;
                    break;
                case AgentEmotionBehaviorType.Happy:
                    result = SpeakerId.WHITECUL_HAPPY;
                    break;
                case AgentEmotionBehaviorType.Disgusted:
                    result = SpeakerId.WHITECUL_SAD;
                    break;
                default: 
                    result = SpeakerId.NONE;
                    break;
            }
            
            return result;
        }
    }
}