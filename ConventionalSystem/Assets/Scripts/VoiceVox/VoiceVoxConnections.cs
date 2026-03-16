using System;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace VoiceVox
{
    /// <summary>
    /// VoiceVoxとの通信を行う
    /// </summary>
    public class VoiceVoxConnections
    {
        /// <summary>
        /// サーバー立ち上げ先のURL
        /// </summary>
        private string _apiURL;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="voicevoxEngineURL"></param>
        public VoiceVoxConnections(string voicevoxEngineURL = "localhost:50021")
        {
            _apiURL = voicevoxEngineURL;
        }
        
        /// <summary>
        /// 音声を作成する
        /// </summary>
        public async UniTask<AudioClip> CreateVoiceRequestAsync(string message,SpeakerId speakerId)
        {
            return await PostSynthesis(message,speakerId);
        }

        /// <summary>
        /// 音声合成
        /// </summary>
        private async UniTask<AudioClip> PostSynthesis(string message,SpeakerId speakerId)
        {
            //リクエストを作成
            using var request = UnityWebRequestMultimedia.GetAudioClip(_apiURL, AudioType.WAV);
            
            //リクエストメソッドを設定
            request.method = "POST";

            var audioQuery = new VoiceVoxModel.RequestModel()
            {
                text = message,
                speaker = speakerId,
                speed = 1.0f,
                pitch = 1.0f,
                intonation = 1.0f,
                volume = 1.0f
            };
            
            var postData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(audioQuery));
            
            //リクエストボディを設定
            request.uploadHandler = new UploadHandlerRaw(postData);
            
            //リクエストヘッダーを設定
            request.SetRequestHeader("Content-Type", "application/json");
            
            ((DownloadHandlerAudioClip) request.downloadHandler).streamAudio = true;

            //リクエストを送信
            await request.SendWebRequest();

            //通信が上手くいったら、レスポンスを受け取って、AudioClipに変換した音声を返す
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                return DownloadHandlerAudioClip.GetContent(request);
            }
        }
    }
}