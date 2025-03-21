using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.Api
{
    public class VoiceBoxApi : ApiBase
    {
        private readonly RestClient _client;
        public VoiceBoxApi()
        {
            this._client = new RestClient();
        }
        public string CreateQuery(string text)
        {
            // Postだけどパラメータを使っているので直接文字列を渡す
            var baseUrl = "http://localhost:50021/audio_query?";
            var addText = $"&text={Uri.EscapeDataString(text)}";
            var addSpeaker = "&speaker=1"; // ずんだもん
            var url = baseUrl + addText + addSpeaker;

            var restRequest = new RestRequest(url, Method.Post);

            var response = this._client.Execute(restRequest);
            Validate(response);
            if (response.Content == null)
            {
                throw new Exception("APIの呼び出しに失敗しました。");
            }

            return response.Content;
        }

        public byte[] GenerateAudio(string query)
        {
            // Postだけどパラメータを使っているので直接文字列を渡す
            var baseUrl = "http://localhost:50021/synthesis?";
            var addSpeaker = "&speaker=1"; // ずんだもん
            var addEnableInterrogativeUpspeak = "&enable_interrogative_upspeak=true";
            var url = baseUrl + addSpeaker + addEnableInterrogativeUpspeak;

            var restRequest = new RestRequest(url, Method.Post);
            restRequest.AddHeader("accept", "audio/wav");
            restRequest.AddParameter("application/json", query, ParameterType.RequestBody);

            var response = this._client.Execute(restRequest);
            Validate(response);
            if (response.RawBytes == null)
            {
                throw new Exception("音声データが空でした");
            }

            return response.RawBytes;
        }
        protected override void Validate(RestResponse res)
        {
            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("リクエスト：" + res.Request);
                Console.WriteLine("内容：" + res.Content);
                throw new Exception("APIの呼び出しに失敗しました。VOICE BOXを立ち上げてください。");
            }
        }
    }
}
