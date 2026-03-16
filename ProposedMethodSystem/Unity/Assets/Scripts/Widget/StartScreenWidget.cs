using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenWidget : MonoBehaviour
{
    public IReactiveProperty<bool> isButtonClicked => isButtonClickedProperty;
    private ReactiveProperty<bool> isButtonClickedProperty = new ReactiveProperty<bool>(false);
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private Button _startButton;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _startButton
            .OnClickAsObservable()
            .Subscribe(_ =>
            {
                isButtonClickedProperty.Value = true;
                
                _canvasGroup.alpha = 0.0f;
                _canvasGroup.interactable = false;
            })
            .AddTo(this.gameObject);
    }
}
