using Dialogue;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class AgentUtteranceTextWidget : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Text _responseText;
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SerializeField] private TargetDeviceTypeDropdownWidget _targetDeviceTypeDropdownWidget;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private DialogueManager _dialogueManager;
    
    
    private void Start()
    {
        _dialogueManager
            .OnCurrentAgentUtterance
            .SkipLatestValueOnSubscribe()
            .Subscribe(x => _responseText.text = _targetDeviceTypeDropdownWidget.OnSelectedDeviceType.Value+ " "+"「"+x+"」")
            .AddTo(this.gameObject);
    }
}