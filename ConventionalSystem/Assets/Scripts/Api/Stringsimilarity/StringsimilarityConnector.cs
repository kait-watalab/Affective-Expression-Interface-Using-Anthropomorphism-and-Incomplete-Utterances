using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Stringsimilarity
{
    /// <summary>
    /// ITAPの権限を管理する
    /// </summary>
    public class StringsimilarityConnector : MonoBehaviour
    {
        /// <summary>
        /// サーバー立ち上げ先のURL
        /// </summary>
        [SerializeField] private string _stringsimilarityURL = "http://vps.watalab.info:18039/stringsimilarity";
        
        /// <summary>
        /// ApplianceStorageInformationDataBaseConnection
        /// </summary>
        private StringsimilarityConnection _stringsimilarityConnection;

        /// <summary>
        /// 
        /// </summary>
        public void Awake()
        {
            _stringsimilarityConnection = new StringsimilarityConnection(_stringsimilarityURL);
        }

        /// <summary>
        /// ITAPの権限を取得する
        /// </summary>
        public async UniTask<StringsimilarityModel.ResponseModel> PostCheckStringSimilarity(List<string> baseDocs,string compareDoc)
        {
            return await _stringsimilarityConnection.PostStringSimilarityRequestAsync(baseDocs,compareDoc);
        }
    }
}