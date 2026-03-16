using Dialogue;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TargetDeviceTypeDropdownWidget : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public IReactiveProperty<string> OnSelectedDeviceType => onSelectedDeviceTypeProperty;
    private StringReactiveProperty onSelectedDeviceTypeProperty = new StringReactiveProperty();
    
    /// <summary>
    /// 
    /// </summary>
   [SerializeField] private Dropdown _deviceTypeDropdown;
   
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
   public TargetDeviceType GetSelectTargetDeviceType()
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

       onSelectedDeviceTypeProperty.Value = _deviceTypeDropdown.options[_deviceTypeDropdown.value].text;
       return targetDeviceType;
   }
}
