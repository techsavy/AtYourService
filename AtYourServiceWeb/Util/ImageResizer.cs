

namespace AtYourService.Web.Util
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    public enum Size
    {
        Thumbnail,
        Small,
        Medium,
        Large
    }

    public class ImageResizer
    {
        public byte[] Resize(byte[] fileContent, Size size)
        {
            using (var image = Image.FromStream(new MemoryStream(fileContent)))
            {
                var ratio = GetResizeRatio(size, image.Width, image.Height);
                var width = (int)(image.Width * ratio);
                var height = (int)(image.Height * ratio);
                var bitmap = new Bitmap(width, height);
                var g = Graphics.FromImage(bitmap);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var rect = new Rectangle(0, 0, width, height);
                g.DrawImage(image, rect);
                var outputBuffer = new MemoryStream();
                bitmap.Save(outputBuffer, image.RawFormat);

                return outputBuffer.ToArray();
            }
        }

        private double GetResizeRatio(Size size, int width, int height)
        {
            double maxHeight = height, maxWidth = width;

            switch (size)
            {
                case Size.Thumbnail:
                    maxHeight = 60;
                    maxWidth = 90;
                    break;

                case Size.Small:
                    maxHeight = 100;
                    maxWidth = 150;
                    break;

                case Size.Medium:
                    maxHeight = 200;
                    maxWidth = 300;
                    break;

                case Size.Large:
                    maxHeight = 400;
                    maxWidth = 600;
                    break;
            }

            return Math.Min(maxWidth / width, maxHeight / height);
        }
    }
}