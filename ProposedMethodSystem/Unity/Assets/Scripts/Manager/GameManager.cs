using Cysharp.Threading.Tasks;
using Dialogue;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VoiceVox;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 現在のゲームの状態
    /// </summary>
    public IReactiveProperty<GameEnum.State> CurrentStateProp => _currentStateProp;
    private ReactiveProperty<GameEnum.State> _currentStateProp = new ReactiveProperty<GameEnum.State>();
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private DialogueManager _dialogueManager;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private TimerManager _timerManager;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private VOICEVOX _voiceVox;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _startButton;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private TargetDeviceTypeDropdownWidget _targetDeviceTypeDropdownWidget;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private AgentProvider _agentProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    //[SerializeField] private ImmersalActiveMachine _immersalActiveMachine;
    
    private void Start()
    {
        _currentStateProp
            .DistinctUntilChanged()
            .Subscribe(async x=>await OnStateChanged(x))
            .AddTo(this.gameObject);
        
        _startButton
            .OnClickAsObservable()
            .Subscribe(_=> _currentStateProp.Value = GameEnum.State.Ready)
            .AddTo(this.gameObject);
    }
    
    /// <summary>
    /// 状態が変化した
    /// </summary>
    /// <param name="currentState">現在の状態</param>
    private async UniTask OnStateChanged(GameEnum.State currentState)
    {
        switch (currentState)
        {
            case GameEnum.State.Ready:
                Ready();
                break;
            
            case GameEnum.State.PlayAsync:
                await PlayAsync();
                break;
                        
            case GameEnum.State.Finished:
                Finished();
                break;
            
            default:
                break;
        }
    }

    private void Ready()
    {
        _agentProvider
            .GetAgent(_targetDeviceTypeDropdownWidget.OnSelectedDeviceType.Value)
            .Subscribe(x => _currentStateProp.Value = GameEnum.State.PlayAsync)
            .AddTo(this.gameObject);
    }
    
    /// <summary>
    /// 
    /// </summary>
    private async UniTask  PlayAsync()
    {
        _dialogueManager
            .IsLastUtterance
            .Where(x=> x == true)
            .Subscribe(_=>  _currentStateProp.Value = GameEnum.State.Finished)
            .AddTo(this.gameObject);
        
        _timerManager.StartCountTime();
        
        await _dialogueManager.StartDialogue(_targetDeviceTypeDropdownWidget.OnSelectedDeviceType.Value);
    }
    
    private void Finished()
    {
        _dialogueManager.StopDialogue();
        _timerManager.StopCountTime();
    }
}
