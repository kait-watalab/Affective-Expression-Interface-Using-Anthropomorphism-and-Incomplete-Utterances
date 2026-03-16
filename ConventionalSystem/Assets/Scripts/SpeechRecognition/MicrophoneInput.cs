using System;
using UnityEngine;
using UniRx;

namespace SpeechRecognition
{
    public class MicrophoneInput : MonoBehaviour
    {
        public ReactiveCommand<byte[]> OnMaxLevelChangeCommand = new();
        
        private const int SampleWindow = 128;
       
        private const float VoiceThreshold = 0.1f;
        private const float VoiceStartThreshold = 0.01f;
        private const float VoiceEndThreshold = 0.005f;
        private const float VADTimeout = 0.1f; 

        private AudioClip microphoneClip;
        private float lastVoiceDetectedTime;
        private bool isVoiceActive = false;   

        private string _deviceName = "UAB-80";

        private void FixedUpdate()
        {
            var maxLevel = CheckMaxLevel();
           
            if (!isVoiceActive && maxLevel > VoiceStartThreshold)
            {
                Debug.Log($"🎤 Voice detected! Level={maxLevel}");
                isVoiceActive = true;
                lastVoiceDetectedTime = Time.time;
            }
            else if (isVoiceActive && maxLevel < VoiceEndThreshold)
            {
                // 声が一定以下になってからしばらく経ったら終了扱い
                if (Time.time - lastVoiceDetectedTime > VADTimeout)
                {
                    var microphoneData = GetMicrophoneData();
                    if (microphoneData != null)
                    {
                        OnMaxLevelChangeCommand.Execute(microphoneData);   
                    }
                        
                    StopRecording();
                    
                    isVoiceActive = false;
                    Debug.Log("🔇 Voice ended");
                }
            }
            else if (isVoiceActive)
            {
                lastVoiceDetectedTime = Time.time;
            }
        }

        private float CheckMaxLevel()
        {
            float maxLevel = 0f;
            float[] samples = new float[SampleWindow];
            int startPosition = Microphone.GetPosition(_deviceName) - SampleWindow + 1;
            if (startPosition > 0)
            {
                microphoneClip.GetData(samples, startPosition);

                foreach (var sample in samples)
                {
                    float absSample = Mathf.Abs(sample);
                    if (absSample > maxLevel)
                    {
                        maxLevel = absSample;
                    }
                }
            }
            
            return maxLevel;
        }

        private byte[] GetMicrophoneData()
        {
            if (Microphone.GetPosition(_deviceName) <= 0)
            {
                return null;
            }
            else
            {
                float[] samples = new float[microphoneClip.samples * microphoneClip.channels];
                microphoneClip.GetData(samples, 0);
                byte[] audioData = new byte[samples.Length * 2];
                for (int i = 0; i < samples.Length; i++)
                {
                    short sample = (short)(samples[i] * short.MaxValue);
                    byte[] sampleBytes = BitConverter.GetBytes(sample);
                    audioData[i * 2] = sampleBytes[0];
                    audioData[i * 2 + 1] = sampleBytes[1];
                }

                return audioData;
            }
        }
        
        public void StartRecording()
        {
            if (Microphone.IsRecording(_deviceName)) return;

            microphoneClip = Microphone.Start(_deviceName, true, 20, 16000);
            lastVoiceDetectedTime = Time.time;
        }

        public void StopRecording()
        {
            if (!Microphone.IsRecording(_deviceName)) return;

            Microphone.End(_deviceName);
        }
    }
}