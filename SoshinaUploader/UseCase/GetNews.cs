using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoshinaUploader.UseCase
{
    public class GetNews
    {
        public string Handle()
        {
            // ニュースを取得
            return new Api.NewsApi().GetTodayNews();
        }
    }
}
