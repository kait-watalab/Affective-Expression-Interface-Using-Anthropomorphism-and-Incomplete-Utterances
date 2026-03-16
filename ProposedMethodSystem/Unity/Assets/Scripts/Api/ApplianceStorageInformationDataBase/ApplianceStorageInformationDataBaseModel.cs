using System;
using System.Collections.Generic;

namespace ApplianceStorageInformationDataBase
{
    /// <summary>
    /// ITAP権限のリクエスト・レスポンス
    /// </summary>
    public class ApplianceStorageInformationDataBaseModel
    {
        /// <summary>
        /// APIのレスポンスを受け取る
        /// </summary>
        [System.Serializable]
        public class ResponseModel
        {
            public List<Product> items { get; set; }
            public Product product { get; set; }

            public class Product
            {
                /// <summary>
                /// トークン
                /// </summary>
                public string productName;

                /// <summary>
                /// パスワード
                /// </summary>
                public DateTime? expirationDate;

                /// <summary>
                /// 
                /// </summary>
                public string fillLevel;
                
                /// <summary>
                /// 
                /// </summary>
                public string dryness;

                /// <summary>
                /// 
                /// </summary>
                public string jancode;

                /// <summary>
                /// 
                /// </summary>
                public string productImageUrl;

                /// <summary>
                /// 
                /// </summary>
                public string base64Image;
            }
        }

        /// <summary>
        /// APIにリクエストを出す
        /// </summary>
        [System.Serializable]
        public class RequestModel
        {
            /// <summary>
            /// ユーザのタイプ
            /// </summary>
            public string productName;

            /// <summary>
            /// ユーザのタイプ
            /// </summary>
            public int expirationDate;

            /// <summary>
            /// ユーザのタイプ
            /// </summary>
            public string jancode;

            /// <summary>
            /// ユーザのタイプ
            /// </summary>
            public string productImageUrl;
        }
    }
}