using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenWidget : MonoBehaviour
{
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
                _canvasGroup.alpha = 0.0f;
                _canvasGroup.interactable = false;
            })
            .AddTo(this.gameObject);
    }
}
