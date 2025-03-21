using SoshinaUploader.Api;
using SoshinaUploader.UseCase;

namespace TestProject
{
    [TestClass]
    public sealed class Test
    {
        [TestMethod]
        public void GetNews()
        {
            var newsText = new GetNews().Handle();
            Assert.IsTrue(!string.IsNullOrEmpty(newsText));
        }

        [TestMethod]
        public void GenerateSoshinaText()
        {
            var text = $@"
タイトル: S&P 500 rises ahead of Federal Reserve's rate decision: Live updates - CNBC
内容: The S&P 500 rose Wednesday as the Federal Reserve's interest rate decision looms.
URL: https://www.cnbc.com/2025/03/18/stock-market-today-live-updates.html";
            var scriptText = new GenerateSoshinaText().Handle(text);
            Assert.IsTrue(!string.IsNullOrEmpty(scriptText));
        }

        [TestMethod]
        public void GenerateAudioFile()
        {
            var scriptText = $@"
今日のニュースはこれ。CNBCによれば、S&P 500が水曜日に上昇しました。これは、連邦準備制度理事会（Federal Reserve）の金利決定が間近に迫っている影響だそうです。市場はこの重大な決定を前に、どんな動きを見せるのか注目されています。
さて、これにはどう考えたらよいか…投資家たちが連邦準備制度の動きを注視することで、市場がどれほどダイナミックで早く変化する可能性を秘めているかが分かりますね。S&P 500の上昇は、将来に向けた楽観的な見方があることを示しているのかもしれません。投資家にとって、今がエキサイティングな時期であることは間違いないですよ。
ただあ！！S&P 500は上昇しているけれど、みんなが一斉に金利発表を待ち構えるなんて、まるで学生がテストの結果を祈りながら待っているようなもんじゃないですか。結局、他人の決断に依存しすぎていては、自分の力を信じる投資家魂を忘れてしまうよ。もっと自分の直感とリスクを信じて、主体的に動いてみたりしないと、いつまで経っても市場の波に振り回されるだけだと思うんですが、どうなんでしょうかね。
";
            var audioPath = new GenerateAudioFile().Handle(scriptText);
            Assert.IsTrue(File.Exists(audioPath));
            File.Delete(audioPath);
        }

        [TestMethod]
        public void ConvertAudioToVideo()
        {
            var videoPath = new ConvertAudioToVideo().Handle("File/audio.wav");
            Assert.IsTrue(File.Exists(videoPath));
            File.Delete(videoPath);
        }
        [TestMethod]
        public void UploadVideo()
        {
            new UploadVideo().Handle("File/video.mp4");
        }
    }
}
