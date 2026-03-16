using UnityEngine;

namespace Utility
{
    /// <summary>
    /// DebugLogのUtility
    /// </summary>
    public static class DebugUtility
    {
        /// <summary>
        /// エラーログ
        /// </summary>
        public static void LogError(string message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }

        /// <summary>
        /// デバッグログ
        /// </summary>
        public static void Log(string message)
        {
            {
#if UNITY_EDITOR
                Debug.Log(message);
#endif
            }
        }
    }
}