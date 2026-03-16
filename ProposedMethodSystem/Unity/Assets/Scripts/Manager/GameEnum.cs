/// <summary>
/// ゲームの状態を管理する
/// </summary>
public static class GameEnum
{
    /// <summary>
    /// 状態
    /// </summary>
    public enum State
    {
        None,
        
        /// <summary>
        /// 準備
        /// </summary>
        Ready,
        
        /// <summary>
        /// 
        /// </summary>
        PlayAsync,
        
        /// <summary>
        /// ゲーム終了
        /// </summary>
        Finished,
    }
}