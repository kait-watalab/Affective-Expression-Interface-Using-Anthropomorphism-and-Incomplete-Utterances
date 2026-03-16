using System.Collections.Generic;
using System.Linq;
using AgentEmotionBehavior;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using SpeechRecognition;
using Stringsimilarity;
using Utility;
using VoiceVox;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyReactiveProperty<string> OnCurrentAgentUtterance => _onCurrentAgentUtteranceProp;
        private StringReactiveProperty _onCurrentAgentUtteranceProp = new StringReactiveProperty();
        
        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyReactiveProperty<bool> IsLastUtterance => _isLastUtteranceProp;
        private BoolReactiveProperty _isLastUtteranceProp = new BoolReactiveProperty(false);
        private bool _isLastUtterance => _isLastUtteranceProp.Value;
        
        /// <summary>
        /// 
        /// </summary>
        private DialogueData _onCurrentDialogueData = new DialogueData();
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private List<DialogueDataBase> _dialogueDataBaseList = new List<DialogueDataBase>();
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private SpeechRecognitionManager _speechRecognition;
        
        /// <summary>
        /// VOICEVOX
        /// </summary>
        [SerializeField] private VOICEVOX _voicevox;
        
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private StringsimilarityConnector _stringsimilarityConnector;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField, Range(0.0f, 1.0f)] private float bestSimilarity = 0.7f;
        
        /// <summary>
        /// 
        /// </summary>
        private CompositeDisposable disposables = new CompositeDisposable();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetDeviceType"></param>
        public async UniTask StartDialogue(TargetDeviceType targetDeviceType)
        {
            var dialogueDataBase = _dialogueDataBaseList.FirstOrDefault(db => db.TargetDeviceType == targetDeviceType);
            
            _speechRecognition
                .OnRecognizeSpeech
                .Where(_ => _voicevox.IsPlaying.Value == false)
                .Where(_ => _isLastUtterance != true)
                .Subscribe(async userSpeech =>
                {
                    if (userSpeech == "")
                    {
                        var agentUtteranceData = new AgentUtterancesData("", AgentEmotionBehaviorType.None);
                        agentUtteranceData = dialogueDataBase.GetAskingBackAgentResponse();
                        
                        _onCurrentAgentUtteranceProp.Value = agentUtteranceData.AgentUtterance;

                        var speakerId =
                            AgentEmotionBehaviorTypeToSpeakerIdConverter.Convert(agentUtteranceData
                                .AgentEmotionBehaviorType);
                        await _voicevox.Speak(agentUtteranceData.AgentUtterance, speakerId);
                    }
                    else
                    {
                        var userExpectedUtterances = _onCurrentDialogueData.UserExpectedUtterances;
                        var responseModel =
                            await _stringsimilarityConnector.PostCheckStringSimilarity(userExpectedUtterances,
                                userSpeech);

                        var agentUtteranceData = new AgentUtterancesData("", AgentEmotionBehaviorType.None);

                        if (responseModel.best_score >= bestSimilarity)
                        {
                            _onCurrentDialogueData = dialogueDataBase.GetDialogueData(_onCurrentDialogueData.Index + 1);
                            agentUtteranceData = _onCurrentDialogueData.AgentUtteranceData;
                        }
                        else
                        {
                            agentUtteranceData = dialogueDataBase.GetAskingBackAgentResponse();
                        }

                        _onCurrentAgentUtteranceProp.Value = agentUtteranceData.AgentUtterance;

                        var speakerId =
                            AgentEmotionBehaviorTypeToSpeakerIdConverter.Convert(agentUtteranceData
                                .AgentEmotionBehaviorType);
                        await _voicevox.Speak(agentUtteranceData.AgentUtterance, speakerId);

                        if (dialogueDataBase.IsLastDialogue(_onCurrentDialogueData.Index))
                        {
                            _isLastUtteranceProp.Value = true;
                        }
                    }
                }).AddTo(disposables);

            _voicevox
                .IsPlaying
                .SkipLatestValueOnSubscribe()
                .Where(_=> _isLastUtterance != true)
                .Subscribe(x =>
                {
                    if (x != true)
                    {
                        _speechRecognition.StartRecognition();
                    }
                }).AddTo(disposables);

            await PlayFirstDialogue(dialogueDataBase);
        }

        public void StopDialogue()
        {
            _speechRecognition.StopRecognition();
            _voicevox.Stop();
            disposables.Dispose();
        }

        private async UniTask PlayFirstDialogue(DialogueDataBase dialogueDataBase)
        {
            var firstDialogueData = dialogueDataBase.GetDialogueData(0);
            var firstAgentUtteranceData = firstDialogueData.AgentUtteranceData;
            
            _onCurrentAgentUtteranceProp.Value = firstDialogueData.AgentUtteranceData.AgentUtterance;
            _onCurrentDialogueData = firstDialogueData;
            
            await _voicevox.Speak(firstAgentUtteranceData.AgentUtterance,AgentEmotionBehaviorTypeToSpeakerIdConverter.Convert(firstAgentUtteranceData.AgentEmotionBehaviorType));
        }
    }
}