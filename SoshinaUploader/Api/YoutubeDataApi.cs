using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using SoshinaUploader.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.Api
{
    public class YoutubeDataApi
    {
        private ClientSecrets _clientSecrets;
        public YoutubeDataApi()
        {
            this._clientSecrets = new ClientSecrets()
            {
                ClientId = ConfigProvider.GetConfig("youtubeDataApi:clientId"),
                ClientSecret = ConfigProvider.GetConfig("youtubeDataApi:clientSecret")
            };
        }
        public void Upload(Video video, string videoPath)
        {
            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetUserCredential(),
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            using (var fileStream = new FileStream(videoPath, FileMode.Open))
            {
                var videosInsertRequest = service.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                var result = videosInsertRequest.UploadAsync().Result;
            }
        }
        private UserCredential GetUserCredential()
        {
            return GoogleWebAuthorizationBroker.AuthorizeAsync(
                this._clientSecrets,
                new[] { YouTubeService.Scope.YoutubeUpload },
                "user",
                CancellationToken.None
            ).Result;
        }
    }
}
