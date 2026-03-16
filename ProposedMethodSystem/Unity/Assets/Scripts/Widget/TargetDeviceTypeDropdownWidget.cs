using Dialogue;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TargetDeviceTypeDropdownWidget : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<TargetDeviceType> OnSelectedDeviceType => selectedDeviceTypeProperty;
    private ReactiveProperty<TargetDeviceType> selectedDeviceTypeProperty = new  ReactiveProperty<TargetDeviceType>();
    
    /// <summary>
    /// 
    /// </summary>
   [SerializeField] private Dropdown _deviceTypeDropdown;
   
    private void Start()
    {
        _deviceTypeDropdown
            .OnValueChangedAsObservable()
            .Subscribe(_=> selectedDeviceTypeProperty.Value = GetSelectTargetDeviceType())
            .AddTo(this.gameObject);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
   private TargetDeviceType GetSelectTargetDeviceType()
   {
       TargetDeviceType targetDeviceType = TargetDeviceType.plant;

       switch (_deviceTypeDropdown.options[_deviceTypeDropdown.value])
       {
              case Dropdown.OptionData optionData when optionData.text == "観葉植物":
                targetDeviceType = TargetDeviceType.plant;
                break;
              
              case Dropdown.OptionData optionData when optionData.text == "ゴミ箱":
                targetDeviceType = TargetDeviceType.garbage;
                break;
              
              default:
                  break;
       }
       
       return targetDeviceType;
   }
}
