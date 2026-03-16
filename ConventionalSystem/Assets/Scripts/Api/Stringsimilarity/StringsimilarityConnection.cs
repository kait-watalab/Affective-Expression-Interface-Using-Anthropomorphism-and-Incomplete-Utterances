using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Stringsimilarity
{
    /// <summary>
    /// 
    /// </summary>
    public class StringsimilarityConnection
    {
        //リクエスト先
        private string _apiUrl = $"http://vps.watalab.info:18039/stringsimilarity";
        
        public StringsimilarityConnection(string stringsimilarityURL = "localhost:18039/stringsimilarity")
        {
            _apiUrl = stringsimilarityURL;
        }
        
        /// <summary>
        /// 権限を取得する
        /// </summary>
        public async UniTask<StringsimilarityModel.ResponseModel>
            PostStringSimilarityRequestAsync(List<string> baseDocs,string compareDoc)
        {
            
            //リクエストヘッダー
            var headers = new Dictionary<string, string>
            {
                { "Content-type", "application/json" },
            };
            
            var similarityEvaluationData = new StringsimilarityModel.RequestModel()
            {
                base_docs = baseDocs,
                compare_doc = compareDoc
            };
            
            var postData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(similarityEvaluationData));

            //リクエストを作成
            using var request = new UnityWebRequest(_apiUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(postData),
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
                return JsonConvert.DeserializeObject<StringsimilarityModel.ResponseModel>(
                    request.downloadHandler.text);
            }
        }
    }
}