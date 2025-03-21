using RestSharp;
using SoshinaUploader.Api.ResponseEntity;
using SoshinaUploader.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.Api
{
    public class NewsApi: ApiBase
    {
        private readonly RestClient _client;
        public NewsApi()
        {
            this._client = new RestClient("https://newsapi.org/v2/");
        }
        public string GetTodayNews()
        {
            var restRequest = new RestRequest("top-headlines", Method.Get);

            restRequest.AddParameter("country", "us");
            restRequest.AddParameter("from", DateTime.Now.ToString("yyyy-MM-dd"));
            restRequest.AddParameter("sortBy", "popularity");
            restRequest.AddParameter("apiKey", ConfigProvider.GetConfig("newsApi:apiKey"));
            restRequest.AddParameter("pageSize", 1);

            var response = this._client.Execute<NewsResponse>(restRequest);
            Validate(response);

            return AirticleToText(response.Data.articles[0]);
        }

        private void Validate(RestResponse<NewsResponse> res)
        {
            base.Validate(res);

            if (res.Data == null || res.Data.articles.Count == 0)
            {
                throw new Exception("ニュース記事が存在しませんでした。");
            }
        }
        
        private string AirticleToText(Article article)
        {
            return $"タイトル: {article.title}\n" +
                $"内容: {article.description}\n" +
                $"URL: {article.url}\n";
        }
    }
}
