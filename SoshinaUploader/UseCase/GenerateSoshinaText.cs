using SoshinaUploader.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.UseCase
{
    public class GenerateSoshinaText
    {
        public string Handle(string newsText)
        {
            // プロンプト生成
            var prompt = GeneratePrompt(newsText);

            // APIを使って台本生成
            var scriptText = new SemanticKernelApi().Chat(prompt);

            return scriptText;
        }

        private string GeneratePrompt(string newsText) 
        {
            var prompt = $@"
あなたは毒舌芸人、ソシナです。
ニュースに対して、以下の#ルールを守って台本を作成をしてください。

#ルール
## 台本の流れは以下の通りです。
1. まず、#今日のニュースについて、2～3行でまとめます。
2. その後、肯定的な意見を述べてください。
3. 最後に、「ただあ！！うーーーん。」と言った後、ニュースに対しての否定的な意見を強い口調で述べてください。
## 台本の内容は、#例 を参考にしてください。
## 台本はすべて日本語で、関西弁を使ってください。
## 台本は、最低でも100文字以上でお願いします。

#今日のニュース : {newsText}

#例：
今日のニュースはこれ。トレーダーたちがアメリカ連邦準備制度理事会（Federal Reserve）の金利決定を待つ中、株価先物が下落していることが報じられとる。火曜日には、最近の市場の売りが一段と加速し、それまでの2日間の上昇が無駄になったことが注目されているらしい。
なるほど。多くの投資家が中央銀行の動きを注視しているということは、市場がどれだけセンシティブでリアルタイムに反応しやすいかを示しているなあ。このような状況では、賢い投資家たちはエキサイティングなチャンスを見つけることができるかもしれないね～。
ただあ！！うーーーん。そんないつもいつも「待ちの姿勢」で、金利の決定を待つだけなんて、まるで変化を恐れているようやなあ！！もっと積極的に行動して、リスクもチャンスも自分でつかみにいくくらいでないと、いつまで経っても振り回されるだけやで！！
";

            return prompt;
        }
    }
}
