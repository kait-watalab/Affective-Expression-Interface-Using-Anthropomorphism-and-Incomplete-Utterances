using Cysharp.Threading.Tasks;

namespace ApplianceStorageInformationDataBase
{
    /// <summary>
    /// ITAPの権限を管理する
    /// </summary>
    public class ApplianceStorageInformationDataBaseConnector
    {
        /// <summary>
        /// ApplianceStorageInformationDataBaseConnection
        /// </summary>
        private ApplianceStorageInformationDataBaseConnection _applianceStorageInformationDataBaseConnection;

        public ApplianceStorageInformationDataBaseConnector()
        {
            _applianceStorageInformationDataBaseConnection = new ApplianceStorageInformationDataBaseConnection();
        }

        /// <summary>
        /// ITAPの権限を取得する
        /// </summary>
        public async UniTask<ApplianceStorageInformationDataBaseModel.ResponseModel> GetLatentInformation(string type)
        {
            return await _applianceStorageInformationDataBaseConnection.GetLatentInformationRequestAsync(type);
        }
    }
}