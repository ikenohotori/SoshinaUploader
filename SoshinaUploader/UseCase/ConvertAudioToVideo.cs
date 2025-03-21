using FFMpegCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.UseCase
{
    public class ConvertAudioToVideo
    {
        public string Handle(string audioPath)
        {
            GlobalFFOptions.Configure(new FFOptions
            {
                BinaryFolder = @"FFmpeg"
            });

            string imagePath = "Files/img/zunda.jpg";
            string videoPath = "video.mp4";

            // FFmpegを使ってMP4を生成
            FFMpegArguments
                .FromFileInput(imagePath, addArguments: options => options.Loop(1)) // 画像をループ
                .AddFileInput(audioPath) // WAV音声を追加
                .OutputToFile(videoPath, true, options => options
                    .WithVideoCodec("libx264") // ビデオコーデック
                    .WithAudioCodec("aac") // AACでエンコード
                    .WithAudioBitrate(192_000) // オーディオビットレート
                    .UsingShortest()) // 最短の入力に合わせる
                .ProcessSynchronously();

            return videoPath;
        }
    }
}
