using System.Collections.Generic;
using AgentEmotionBehavior;
using UnityEngine;

namespace Dialogue
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/ScenarioDataBase")]
    public class DialogueDataBase : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        public TargetDeviceType TargetDeviceType => _targetDeviceType;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private TargetDeviceType _targetDeviceType　= TargetDeviceType.refrigerator;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<DialogueData> _dialogueDataList = new List<DialogueData>();

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<string> _askingBackAgentResponseList = new List<string>();
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<string> _alternativeExpressionRequestList = new List<string>();
        
        /// <summary>
        /// 
        /// </summary>
        public DialogueData GetDialogueData(int dialogueIndex)
        {
            return _dialogueDataList[dialogueIndex];
        }

        public AgentUtterancesData GetAskingBackAgentResponse()
        {
            return new AgentUtterancesData(_askingBackAgentResponseList[Random.Range(0, _askingBackAgentResponseList.Count)], AgentEmotionBehaviorType.Neutral);
        }
        
        public AgentUtterancesData GetAlternativeExpressionRequest()
        {
            return new AgentUtterancesData(_alternativeExpressionRequestList[Random.Range(0, _alternativeExpressionRequestList.Count)], AgentEmotionBehaviorType.Disgusted);
        }
        
        public bool IsLastDialogue(int dialogueIndex)
        {
            return dialogueIndex == _dialogueDataList.Count-1;
        }
    }
}