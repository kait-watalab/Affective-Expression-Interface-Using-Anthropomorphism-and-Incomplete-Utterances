using Commons.LogData;
using ExperimentalDataBase;
using SpeechRecognition;
using Stringsimilarity;
using UniRx;
using UnityEngine;
using VoiceVox;
using Zenject;

public class DataLogger : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SerializeField] private TimerManager _timerManager;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private SpeechRecognitionManager _speechRecognitionManager;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameManager _gameManager;
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SerializeField] private TargetDeviceTypeDropdownWidget _targetDeviceTypeDropdownWidget;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private ExperimentalDataBaseConnector _experimentalDataBaseConnector;

    
    [SerializeField] private VOICEVOX _vox;
    
    /// <summary>
    /// 
    /// </summary>
    [Inject] private LogDataManager _saveManager;
    
    private void Start()
    {
        _speechRecognitionManager
            .OnRecognizeSpeech
            .Subscribe(recognizedText =>_saveManager.AddUtterancesData(recognizedText,_timerManager.OnCurrentTime.Value))
            .AddTo(this.gameObject);

         _targetDeviceTypeDropdownWidget
             .OnSelectedDeviceType
             .SkipLatestValueOnSubscribe()
             .Subscribe(deviceType => _saveManager.SerTargetDeviceType(deviceType))
             .AddTo(this.gameObject);
        
        _gameManager
            .CurrentStateProp
            .Where(x => x == GameEnum.State.Finished)
            .Subscribe(_ =>
            {
                _saveManager.SetTotalTime(_timerManager.OnCurrentTime.Value);
                _saveManager.Save();
            })
            .AddTo(this.gameObject);

        _saveManager
            .OnSaveComplete
            .Subscribe(async x=> await _experimentalDataBaseConnector.PostExperimentalDataBase(x))
            .AddTo(this.gameObject);

        _speechRecognitionManager
            .OnRecognizeSpeech
            .Subscribe(x => Debug.Log(_timerManager.OnCurrentTime.Value + ":" + x))
            .AddTo(this.gameObject);

        _vox
            .IsPlaying
            .Where(x => x == true)
            .Subscribe(x => Debug.Log(_timerManager.OnCurrentTime.Value + ":音声再生"));
    }
}
