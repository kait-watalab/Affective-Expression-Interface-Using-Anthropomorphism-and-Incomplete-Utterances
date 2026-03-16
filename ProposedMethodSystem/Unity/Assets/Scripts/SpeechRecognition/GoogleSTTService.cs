using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using UniRx;

namespace SpeechRecognition
{
    public class GoogleSTTService : MonoBehaviour
    {
        private const string API_KEY = "AIzaSyC_i5NWmm1z6wJ3OHRdunonFfoaGRoVQLI";
        private const string URL = "https://speech.googleapis.com/v1/speech:recognize?key=";

        private const string Locale =
            // "en-US"
            // "ko-KR"
            "ja-JP";

        public IReactiveCommand<string> OnRecognizeSpeech => _recognizeSpeechCommand;
        private ReactiveCommand<string> _recognizeSpeechCommand = new();

        public async void RecognizeSpeech(byte[] audioData)
        {
            string audioContent = Convert.ToBase64String(audioData);
            string requestJson =
                $"{{\"config\": {{\"encoding\":\"LINEAR16\",\"sampleRateHertz\":16000,\"languageCode\":\"{Locale}\"}},\"audio\":{{\"content\":\"{audioContent}\"}}}}";
            string fullUrl = URL + API_KEY;
            using var request = new UnityWebRequest(fullUrl, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(requestJson);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"GoogleSTTService.RecognizeSpeech() request.error is [{request.error}]");
                _recognizeSpeechCommand.Execute(string.Empty);
            }
            else
            {
                string responseText = request.downloadHandler.text;
                var json = JObject.Parse(responseText);
                
                string transcript = json["results"]?
                    .Select(r => r["alternatives"]?[0]?["transcript"]?.ToString())
                    .LastOrDefault(t => !string.IsNullOrEmpty(t));
                
                if (!string.IsNullOrEmpty(transcript))
                {
                    _recognizeSpeechCommand.Execute(transcript);
                }
            }
        }
    }
}