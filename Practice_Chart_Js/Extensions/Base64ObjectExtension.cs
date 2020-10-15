using MoiraTools.CustomClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoiraTools.Extensions
{
    public static class Base64ObjectExtension
    {
        public static void SaveImage_Png(this Base64Object base64Object, string path)
        {
            if (base64Object == null)
                throw new Exception("base64Object is null !");

            string x = base64Object.Value.Replace("data:image/png;base64,", "");
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(x);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void SaveImage_Jpg(this Base64Object base64Object, string path)
        {
            if (base64Object == null)
                throw new Exception("base64Object is null !");

            string x = base64Object.Value.Replace("data:image/jpeg;base64,", "");
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(x);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            // 因為轉成 jpg 格式，jpg 格式沒有透明，所以先將圖設為 bitmap，將背景改為白色
            // 再轉成 jpg 格式，不然原本透明的地方是整片黑 !!!
            /// ↓這不知道為毛沒用
            //Bitmap bitmap = new Bitmap(image);
            //bitmap.MakeTransparent(Color.White);
            //bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);

            // bitmap 設置畫布
            // Graphics 負責畫
            using (var b = new Bitmap(image.Width, image.Height))
            {
                b.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                // FromImage 就是將畫布設在 bitmap 上的概念
                using (var g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(image, 0, 0);
                }
          
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;//給Compression無效
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 100L);// 壓縮品質 100L，數字愈低，破壞性壓縮愈大

                b.Save(path, jpgEncoder, myEncoderParameters);
            }          
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().Where(m => m.FormatID == format.Guid).FirstOrDefault();
            if (codec == null)            
                return null;
            
            return codec;
        }
    }
}
