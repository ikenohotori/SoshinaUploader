using SoshinaUploader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.UseCase
{
    public class GenerateAudioFile
    {
        private readonly VoiceBoxApi _voiceBoxApi;
        public GenerateAudioFile()
        {
            this._voiceBoxApi = new VoiceBoxApi();
        }
        public string Handle(string scriptText)
        {
            // クエリ作成
            var query = this._voiceBoxApi.CreateQuery(scriptText);

            // 音声ファイル生成
            var audioData = this._voiceBoxApi.GenerateAudio(query);

            // 保存
            var savePath = SaveAudioFile(audioData);

            return savePath;
        }

        private string SaveAudioFile(byte[] audioData)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var savePath = System.IO.Path.Combine(basePath, "output.wav");
            System.IO.File.WriteAllBytes(savePath, audioData);
            return savePath;
        }
    }
}
