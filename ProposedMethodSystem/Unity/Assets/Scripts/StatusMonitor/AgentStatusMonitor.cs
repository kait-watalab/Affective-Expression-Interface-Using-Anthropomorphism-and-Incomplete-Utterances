using System;
using System.Linq;
using ApplianceStorageInformationDataBase;
using Cysharp.Threading.Tasks;
using Dialogue;
using UnityEngine;

public class AgentStatusMonitor : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private ApplianceStorageInformationDataBaseConnector _applianceStorageInformationDataBaseConnector = new ApplianceStorageInformationDataBaseConnector();

    public async UniTask<(bool, string)> StartMonitoring(TargetDeviceType targetDeviceType)
    {
        var deviceType = targetDeviceType;
        var latentInformation =
            await _applianceStorageInformationDataBaseConnector.GetLatentInformation(targetDeviceType.ToString());

        switch (deviceType)
        {
            case TargetDeviceType.refrigerator:

                var expiredFoodProduct = latentInformation
                    .items
                    .Where(p => p.expirationDate < DateTime.Today)
                    .OrderByDescending(p => p.expirationDate)
                    .ToList();

                if (expiredFoodProduct.FirstOrDefault() != null)
                {
                    return (true, expiredFoodProduct.FirstOrDefault().productName);
                }
                else
                {
                    return (false, "");
                }

                break;
            case TargetDeviceType.plant:

                var lastDrynessData = latentInformation.items.LastOrDefault();

                return (true, "");
                if (float.Parse(lastDrynessData.dryness) >= 80)
                {
                    return (true, "");
                }
                else
                {
                    return (false, "");
                }

                break;
            case TargetDeviceType.garbage:

                var lastFillLevelData = latentInformation.items.LastOrDefault();

                if (float.Parse(lastFillLevelData.fillLevel) > 80)
                {
                    return (true, "");
                }
                else
                {
                    return (false, "");
                }

                break;
            default:
                return (false, "");
                break;
        }
    }
}