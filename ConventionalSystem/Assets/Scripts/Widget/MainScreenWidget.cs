using UniRx;
using UnityEngine;

public class MainScreenWidget : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private GameManager _gameManager;
    
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _gameManager
            .CurrentStateProp
            .Where(x => x == GameEnum.State.Play)
            .Subscribe(_ => _canvasGroup.alpha = 1f)
            .AddTo(this.gameObject);
        
        _gameManager
            .CurrentStateProp
            .Where(x => x == GameEnum.State.Finished)
            .Subscribe(_ => _canvasGroup.alpha = 0f)
            .AddTo(this.gameObject);
    }
}
