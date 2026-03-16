using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using Utility;

namespace ExperimentalDataBase
{
    /// <summary>
    /// ITAPの権限を管理するAPIと通信する
    /// </summary>
    public class ExperimentalDataBaseConnection
    {
        //リクエスト先
        private string _apiUrl = "https://script.google.com/macros/s/AKfycbw_jUIxlTnhBGS8B2Vu1Nh0_9sF7I00OtGOc_5h8HI1hJTgYkJkn0mLoCsWnfFolut8AA/exec";

        public ExperimentalDataBaseConnection(string experimentalDataBaseURL)
        {
            _apiUrl = experimentalDataBaseURL;
        }
        
        /// <summary>
        /// 権限を取得する
        /// </summary>
        public async UniTask PostExperimentalDataBasenRequestAsync(string logDataJson)
        {
            
            //リクエストヘッダー
            var headers = new Dictionary<string, string>
            {
                { "Content-type", "application/json" },
            };

            //リクエストを作成
            using var request = new UnityWebRequest(_apiUrl, "POST")
            {
                downloadHandler = new DownloadHandlerBuffer(),
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(logDataJson))
            };

            //リクエストヘッダーを設定
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            //リクエストを送信
            await request.SendWebRequest();

            //通信が上手くいったら、レスポンスを受け取って、権限を返す
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                DebugUtility.LogError(request.error);
                throw new Exception();
            }
            else
            {
                DebugUtility.Log("データの送信が完了しました:" + logDataJson);
            }
        }
    }
}