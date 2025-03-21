using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SoshinaUploader.Util
{
    public static class ConfigProvider
    {
        public static string GetConfig(string key)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) // ローカルファイルはオプション
            .Build();

            if(configuration[key] == null)
            {
                throw new Exception($"設定ファイルに{key}が見つかりません");
            }

            return configuration[key];
        }
    }
}
