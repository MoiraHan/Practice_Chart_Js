using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoiraTools.CustomClass
{
    public class Base64Object
    {
        public string Value { get; }

        public Base64Object(string value)
        {
            if (!IsBase64(value))
                throw new Exception("字串不為 Base64 內容 !");

            Value = value;
        }


        private bool IsBase64(string base64String)
        {
            // 最快的測試。如果該值為 null 或等於 0，則不是 base64
            // Base64 字符串的長度始終可以被 4 整除，即 8、16、20 等。
            // 此外，如果符合上述條件，請測試空格。
            // 如果包含空格，則不是 base64
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
