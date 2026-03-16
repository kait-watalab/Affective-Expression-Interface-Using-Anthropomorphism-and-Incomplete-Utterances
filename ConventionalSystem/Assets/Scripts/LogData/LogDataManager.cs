using System;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UniRx;
using Utility;

namespace Commons.LogData
{
    /// <summary>
    /// セーブマネージャーを管理する
    /// </summary>
    public class LogDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        public IObservable<string> OnSaveComplete => _saveCompleteSubject;
        private Subject<string> _saveCompleteSubject = new Subject<string>();
       
        /// <summary>
        /// セーブするデータ
        /// </summary>
        private LogData LogData;

        /// <summary>
        /// 保存場所
        /// </summary>
        private string Path =>Application.persistentDataPath + $"/LogData_{DateTime.Now:yyyy-MM-dd-HH-mm}.json";
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private LogDataManager()
        { 
            LogData = new LogData();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void SerTargetDeviceType(string targetDeviceType)
        {
            LogData.TargetDeviceType = targetDeviceType;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void SetTotalTime(float totalTime)
        {
            LogData.TotalTime = totalTime;
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void AddUtterancesData(string utterance, float sectionTime)
        {
            LogData.UtterancesData.Add(new UtteranceData(utterance, sectionTime));
        }

        /// <summary>
        /// セーブ
        /// </summary>
        public void Save()
        {
            string jsonData = JsonConvert.SerializeObject(LogData);
            StreamWriter streamWriter = new StreamWriter(Path, false, Encoding.GetEncoding("UTF-8"));
            streamWriter.WriteLine(jsonData);
            streamWriter.Flush();
            streamWriter.Close();
            DebugUtility.Log("セーブ完了");
            
            _saveCompleteSubject.OnNext(jsonData);
            _saveCompleteSubject.OnCompleted();
        }
    }
}