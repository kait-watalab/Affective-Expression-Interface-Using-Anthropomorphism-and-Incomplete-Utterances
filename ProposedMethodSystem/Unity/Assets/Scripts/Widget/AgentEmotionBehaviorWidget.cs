using AgentEmotionBehavior;
using Dialogue;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Widget
{
    public class AgentEmotionBehaviorWidget : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private DialogueManager _dialogueManager;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private AgentEmotionBehaviorDataBase _deviceEmotionDataBase;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private Image _emotionIconImage;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private Image _speechBubbleImage;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private TextMeshProUGUI _utterancText;

        private void Start()
        {
            _dialogueManager
                .OnCurrentAgentUtterancesData
                .SkipLatestValueOnSubscribe()
                .Subscribe(x =>
                {
                    _utterancText.text = x.AgentUtterance;
                    SetEmotion(x.AgentEmotionBehaviorType);
                })
                .AddTo(this.gameObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentEmotionBehaviorType"></param>
        private void SetEmotion(AgentEmotionBehaviorType agentEmotionBehaviorType)
        {
            var deviceEmotionData = _deviceEmotionDataBase.GetAgentEmotionBehaviorData(agentEmotionBehaviorType);
            _emotionIconImage.sprite = deviceEmotionData.FacialExpressionSprite;
            _speechBubbleImage.sprite = deviceEmotionData.SpeechBubbleSprite;
            _utterancText.color = deviceEmotionData.UtterancTextColor;
            _utterancText.font = deviceEmotionData.UtterancTextFont as TMP_FontAsset;
        }
    }
}