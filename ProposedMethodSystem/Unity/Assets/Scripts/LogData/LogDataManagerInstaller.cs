using Zenject;

namespace Commons.LogData
{
    /// <summary>
    /// セーブマネージャーを注入する
    /// </summary>
    public class LogDataManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(LogDataManager))
                .To<LogDataManager>().AsSingle().NonLazy();
        }
    }
}