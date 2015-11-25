using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class ImagesHelper
    {
        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        /// <summary>
        /// Chuyển kiểu Binary thành Image
        /// </summary>
        /// <param name="binaryData"></param>
        /// <returns></returns>
        public static  Image BinaryToImage(System.Data.Linq.Binary binaryData)
        {
            if (binaryData == null) return null;

            byte[] buffer = binaryData.ToArray();
            MemoryStream memStream = new MemoryStream();
            memStream.Write(buffer, 0, buffer.Length);
            return Image.FromStream(memStream);
        }
        /// <summary>
        /// Chuyển kiểu Image thành Binary
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToBinary(Image img)
        {

            MemoryStream stream = new MemoryStream();
            img.Save(stream, ImageFormat.Png);
            BinaryReader streamreader = new BinaryReader(stream);
            var data = stream.GetBuffer();
            return data;
        }
        public static string ImageToBase64String(Image img)
        {
            return Convert.ToBase64String(ImageToBinary(img));
        }

        public static string ImageToDataBase64String(Image img)
        {
            return String.Format("data:image/gif;base64,{0}", ImageToBase64String(img));
        }
    }
}
