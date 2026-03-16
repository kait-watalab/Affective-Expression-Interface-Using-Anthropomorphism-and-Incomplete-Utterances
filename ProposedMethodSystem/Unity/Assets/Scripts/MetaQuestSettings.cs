using System;
using UnityEngine;
using UnityEngine.XR;

public class MetaQuestSettings : MonoBehaviour
{
   private void Awake()
   {
      OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.High;
      
      OVRManager.useDynamicFoveatedRendering = true;
      OVRManager.foveatedRenderingLevel = OVRManager.FoveatedRenderingLevel.High;
      
      XRSettings.eyeTextureResolutionScale = 0.9f;
      
      OVRManager.display.displayFrequency = 90f;
   }
}
