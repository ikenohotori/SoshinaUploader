using Google.Apis.YouTube.v3.Data;
using SoshinaUploader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.UseCase
{
    public class UploadVideo
    {
        public void Handle(string videoPath)
        {
            // 動画をアップロード
            var video = CreateVideo();
            new YoutubeDataApi().Upload(video, videoPath);
        }
        private Video CreateVideo()
        {
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = $@"今日のニュース {DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}";
            video.Snippet.Description = "今日のニュースをずんだもんが斬る";
            video.Snippet.Tags = new string[] { "tag1", "tag2" };
            video.Snippet.CategoryId = "22"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "public";

            return video;
        }
    }
}
