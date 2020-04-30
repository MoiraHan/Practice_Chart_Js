using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice_Chart_Js.Helper
{
    public static class JsonStringHelper
    {
        /// <summary> 將資料從model轉換成json字串( 用於轉換成字串並給前端的 javascript 使用 JSON.parse 的方法，有特別處理特殊字元以及時間問題 ) </summary>
        /// <param name="instance">razor頁面的HtmlHelper</param>
        /// <param name="model">要轉換成 Json 的 Model</param>
        public static IHtmlString ToJsonString(this HtmlHelper instance, object model)
        {
            // 將時間轉成指定的JSON字串格式
            // https://stackoverflow.com/questions/18635599/
            var jsonConverter = new IsoDateTimeConverter() { DateTimeFormat = "yyyy/MM/dd HH:mm" };

            var jsonString = JsonConvert.SerializeObject(model, Formatting.None, jsonConverter);

            // 特別處理特殊字元 ", ', \ 的問題
            // => ' 需要轉換成 \'
            // => " 需要轉換成 \\"
            // => \ 需要轉換成 \\\\
            //   => EX: 將 "aaa'bbb\"ccc\\ddd\n" 轉換成 JSON 字串後
            //   => 輸出會是 "aaa'bbb\\"ccc\\ddd\\n"
            //   => 所以先將 \ 換成 \\  => "aaa'bbb\\\\"ccc\\\\"
            //   => 接著再將 ' 換成 \'  => "aaa\'bbb\\\\"ccc\\\\"
            //   => 如此到前端後就可直接提供 JSON.parse 處理
            jsonString = jsonString
                .Replace("\\", "\\\\")
                .Replace("'", "\\'");

            return instance.Raw(jsonString);
        }
    }
}