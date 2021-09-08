using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace HtmlConvertor.Common.Helpers
{
    public static class ImageHelper
    {
        public static Bitmap CropImage(this Bitmap source, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
            return bmp;
        }
        public static byte[] ToByteArray(this Bitmap image)
        {
            using MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
        public static Bitmap ConvertWebElementToBitmap(this IWebElement webElement, Screenshot screenShot)
        {
            Point point = webElement.Location;
            int width = webElement.Size.Width;
            int height = webElement.Size.Height;
            Rectangle section = new Rectangle(point, new Size(width, height));
            Bitmap source = new Bitmap(new MemoryStream(screenShot.AsByteArray));
            Bitmap finalCaptchaImage = source.CropImage(section);
            return finalCaptchaImage;
        }
    }
}