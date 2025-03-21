using SoshinaUploader.UseCase;

namespace SoshinaUploader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("開始");

            try
            {
                // ニュースを取得
                Console.WriteLine("ニュース取得開始");
                var newsText = new GetNews().Handle();
                Console.WriteLine("ニュース取得完了");

                // 粗品テキストを作成
                Console.WriteLine("テキスト作成開始");
                var scriptText = new GenerateSoshinaText().Handle(newsText);
                Console.WriteLine("テキスト作成完了");

                // 音声ファイルを作成
                Console.WriteLine("音声作成開始");
                var audioPath = new GenerateAudioFile().Handle(scriptText);
                Console.WriteLine("音声作成完了");

                // 動画に変換
                Console.WriteLine("動画変換開始");
                var videoPath = new ConvertAudioToVideo().Handle(audioPath);
                Console.WriteLine("動画変換完了");

                // 動画をアップロード
                Console.WriteLine("動画アップロード開始");
                new UploadVideo().Handle(videoPath);
                Console.WriteLine("動画アップロード完了");

                Console.WriteLine("完了");
            }
            catch (Exception ex)
            {
                Console.WriteLine("失敗");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
