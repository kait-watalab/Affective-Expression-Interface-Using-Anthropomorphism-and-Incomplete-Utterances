using System;
using System.Linq;
using UnityEngine;
using UniRx;

namespace SpeechRecognition
{
    public class SpeechRecognitionManager : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private MicrophoneInput _microphoneInput;

        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private GoogleSTTService _googleSTTService;

        /// <summary>
        /// 
        /// </summary>
        public IReactiveCommand<string> OnRecognizeSpeech => _onRecognizeSpeechCommand;
        
        /// <summary>
        /// 
        /// </summary>
        private ReactiveCommand<string> _onRecognizeSpeechCommand = new ReactiveCommand<string>();
        
        private void Awake()
        {
            _microphoneInput.OnMaxLevelChangeCommand
                .Subscribe(_googleSTTService.RecognizeSpeech)
                .AddTo(this);

            _googleSTTService
                .OnRecognizeSpeech
                .DistinctUntilChanged()
                .Subscribe(x=>_onRecognizeSpeechCommand.Execute(x))
                .AddTo(this);
        }
        
        public void StartRecognition() => _microphoneInput.StartRecording();
        
        public void StopRecognition() => _microphoneInput.StopRecording();
    }
}