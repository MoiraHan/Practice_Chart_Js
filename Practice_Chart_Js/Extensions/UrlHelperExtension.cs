using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CSF.IST.Workshop.Web.Framework.Extensions
{
    public static class UrlHelperExtension
    {
        /// <summary>
        /// 路徑導正。測試時，放檔案的資料夾都是根目錄，但其實發佈的 Server 上是在某個網域下的某個目錄
        /// Ex. 測試時放檔案路徑會寫 /TempFiles，但是發佈到 Server 上其實路徑是 www.xxxxx.com.tw/WebSiteA/TempFiles
        /// 這樣寫 /TempFiles 時會錯，可用此方法抓到前面部份的正確位置
        /// </summary>
        public static string Absolute(this UrlHelper url, string relativeOrAbsolute)
        {
            var uri = new Uri(relativeOrAbsolute, UriKind.RelativeOrAbsolute);
            // 如果給是絕對路徑，就不處理
            if (uri.IsAbsoluteUri)
                return relativeOrAbsolute;

            return ResolveServerUrl(VirtualPathUtility.ToAbsolute(relativeOrAbsolute), false);
        }

        static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            // 再次檢查(?) 如果給是絕對路徑，就不處理
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) + "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
    }
}
