using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.Api
{
    public abstract class ApiBase
    {
        protected virtual void Validate(RestSharp.RestResponse res)
        {
            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("リクエスト：" + res.Request);
                Console.WriteLine("内容：" + res.Content);

                throw new Exception("APIの呼び出しに失敗しました。");
            }
        }
    }
}
