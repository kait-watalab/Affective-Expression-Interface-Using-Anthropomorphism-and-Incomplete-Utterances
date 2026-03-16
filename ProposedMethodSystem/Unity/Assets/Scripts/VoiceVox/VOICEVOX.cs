using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace VoiceVox
{
    [RequireComponent(typeof(AudioSource))]
    public class VOICEVOX : MonoBehaviour
    {
        /// <summary>
        /// 再生中かどうかの公開フラグ
        /// </summary>
        public IReactiveProperty<bool> IsPlaying => _isPlayingProp;
        private BoolReactiveProperty _isPlayingProp = new BoolReactiveProperty(false);
        
        /// <summary>
        /// AudioSource
        /// </summary>
        [SerializeField] private AudioSource _audioSource;
        
        /// <summary>
        /// サーバー立ち上げ先のURL
        /// </summary>
        [SerializeField] private string _voicevoxEngineURL = "http://vps.watalab.info:18039/voicevox/syntheticspeech";
        
        /// <summary>
        /// VoiceVoxとの通信を行う
        /// </summary>
        private VoiceVoxConnections _voiceVox;
        
        private void Awake()
        {
            _audioSource = gameObject.GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

            _voiceVox = new VoiceVoxConnections(_voicevoxEngineURL);
        }

        /// <summary>
        /// 音声データを作る
        /// </summary>
        /// <param name="speakerId">話者</param>
        /// <param name="message">音声にする内容</param>
        /// <returns></returns>
        private async UniTask<AudioClip> CreateVoice(string message,SpeakerId speakerId)
        {
            return await _voiceVox.CreateVoiceRequestAsync(message,speakerId);
        }
        
        /// <summary>
        /// 音声を流す
        /// </summary>
        /// <param name="audioClip">音声データ</param>
        private async UniTask Play(AudioClip audioClip)
        {
            _isPlayingProp.Value = true;
            
            _audioSource.clip = audioClip;
            _audioSource.Play();
            
            await UniTask.WaitUntil(() => _audioSource.isPlaying);
            await UniTask.WaitUntil(() => !_audioSource.isPlaying);
            
            _isPlayingProp.Value = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
            _isPlayingProp.Value = false;
        }

        /// <summary>
        /// 話す
        /// </summary>
        public async UniTask Speak(string message,SpeakerId speakerId)
        {
            //伝えたい内容から音声を生成する
            var audioClip = await CreateVoice(message,speakerId);
            
            //音声を再生する
            await Play(audioClip);
        }
    }
}