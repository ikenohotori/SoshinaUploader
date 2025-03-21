using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SoshinaUploader.Util;

namespace SoshinaUploader.Api
{
    public class SemanticKernelApi
    {
        private string _apiKey;
        private string _modelId;

        public SemanticKernelApi()
        {
            this._apiKey = ConfigProvider.GetConfig("semanticKernel:apiKey");
            this._modelId = ConfigProvider.GetConfig("semanticKernel:modelId");
        }
        public string Chat(string prompt)
        {
            // Semantic Kernelのインスタンスを作成
            var builder = Kernel.CreateBuilder().AddOpenAIChatCompletion(this._modelId, this._apiKey);
            Kernel kernel = builder.Build();
            var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

            var result = chatCompletionService.GetChatMessageContentsAsync(prompt).Result;
            if (result == null || result.Count == 0)
            {
                return "OpenAIの結果エラーです";
            }
            if (result[0].Content == null)
            {
                return "OpenAIのメッセージが空です";
            }

            return result[0].Content;
        }
    }
}