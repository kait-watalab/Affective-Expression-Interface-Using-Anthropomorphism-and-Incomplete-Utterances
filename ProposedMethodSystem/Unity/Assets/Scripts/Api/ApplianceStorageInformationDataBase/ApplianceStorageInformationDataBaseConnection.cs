using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ApplianceStorageInformationDataBase
{
    /// <summary>
    /// ITAPの権限を管理するAPIと通信する
    /// </summary>
    public class ApplianceStorageInformationDataBaseConnection
    {
        //リクエスト先
        string _apiUrl =
            $"https://script.google.com/macros/s/AKfycbztFqfr2iC-7evv-Y-U035Np1HQqfqoArPndjtn64YUM3UVZ0N5jf97aNn3zs-yOx_zhw/exec";
        
        /// <summary>
        /// 権限を取得する
        /// </summary>
        /// 取得する情報の家電を指定できるようにする
        public async UniTask<ApplianceStorageInformationDataBaseModel.ResponseModel>
            GetLatentInformationRequestAsync(string type)
        {
            
            //リクエストヘッダー
            var headers = new Dictionary<string, string>
            {
                { "Content-type", "application/json" },
            };
            
            string fullUrl = _apiUrl + $"?type={type}";

            //リクエストを作成
            using var request = new UnityWebRequest(fullUrl, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer()
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
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                return JsonConvert.DeserializeObject<ApplianceStorageInformationDataBaseModel.ResponseModel>(
                    request.downloadHandler.text);
            }
        }
    }
}