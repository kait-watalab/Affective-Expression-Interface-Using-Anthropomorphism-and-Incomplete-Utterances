using System.Collections.Generic;

namespace Stringsimilarity
{
    /// <summary>
    /// ITAP権限のリクエスト・レスポンス
    /// </summary>
    public class StringsimilarityModel
    {
        /// <summary>
        /// APIのレスポンスを受け取る
        /// </summary>
        [System.Serializable]
        public class ResponseModel
        {
            /// <summary>
            /// トークン
            /// </summary>
            public string best_doc;

            /// <summary>
            /// 
            /// </summary>
            public float best_score;

            /// <summary>
            /// 
            /// </summary>
            public string compare_doc;
        }
        
        /// <summary>
        /// APIにリクエストを出す
        /// </summary>
        [System.Serializable]
        public class RequestModel
        {
            /// <summary>
            /// トークン
            /// </summary>
            public List<string> base_docs;
            
            /// <summary>
            /// 
            /// </summary>
            public string compare_doc;
        }
    }
}