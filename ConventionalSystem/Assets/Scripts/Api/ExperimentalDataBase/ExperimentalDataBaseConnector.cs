using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ExperimentalDataBase
{
    /// <summary>
    /// ITAPの権限を管理する
    /// </summary>
    public class ExperimentalDataBaseConnector : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        [SerializeField] private string experimentalDataBaseURL = "https://script.google.com/macros/s/AKfycbw_jUIxlTnhBGS8B2Vu1Nh0_9sF7I00OtGOc_5h8HI1hJTgYkJkn0mLoCsWnfFolut8AA/exec";
        
        /// <summary>
        /// ApplianceStorageInformationDataBaseConnection
        /// </summary>
        private ExperimentalDataBaseConnection _experimentalDataBaseConnection;

        /// <summary>
        /// 
        /// </summary>
        public void Awake()
        {
            _experimentalDataBaseConnection = new ExperimentalDataBaseConnection(experimentalDataBaseURL);
        }

        /// <summary>
        /// ITAPの権限を取得する
        /// </summary>
        public async UniTask PostExperimentalDataBase(string logDataJson)
        {
            await _experimentalDataBaseConnection.PostExperimentalDataBasenRequestAsync(logDataJson);
        }
    }
}