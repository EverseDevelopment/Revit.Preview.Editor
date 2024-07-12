using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EditRevitFile.Preview
{
    public static class PreviewEdit
    {
        public static void Run(string path, string imageLoc)
        {
            string pngFilePath = imageLoc;
            string newPath = Path.Combine(path, "RevitPreview4.0");

            using (var bmp = Bitmap.FromFile(pngFilePath))
            {
                // convert
                var bytes = Convert(bmp);
                // save
                File.WriteAllBytes(newPath, bytes);
            }
        }

        private static byte[] Convert(Image sourceImg)
        {
            using (var outputStream = new MemoryStream())
            {
                // read and save prefix
                const string prefixFileName = "template.bin";
                var bytes = LoadResource(prefixFileName);
                outputStream.Write(bytes, 0, bytes.Length);

                // convert image to 128x128 Thumbnail Image with white background
                const int size = 128;
                using (var result = ThumbnailImage(sourceImg, size, Color.White, 72))
                {
                    // encode image to PNG
                    result.Save(outputStream, ImageFormat.Png);
                }

                // read and save postfix
                const string postfixFileName = "template2.bin";
                bytes = LoadResource(postfixFileName);
                outputStream.Write(bytes, 0, bytes.Length);

                return outputStream.ToArray();
            }
        }

        private static byte[] LoadResource(string prefixFileName)
        {
            return File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, prefixFileName));
        }

        private static Image ThumbnailImage(Image sourceImage, int imageSize, Color backgroundColor, int dpi, System.Drawing.Drawing2D.InterpolationMode interpolation = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor)
        {
            int thumbnailWidth = imageSize;
            int thumbnailHeight = imageSize;

            float aspectRatio = (float)sourceImage.Width / sourceImage.Height;
            float targetAspectRatio = (float)thumbnailWidth / thumbnailHeight;

            if (aspectRatio < targetAspectRatio)
            {
                thumbnailWidth = (int)(thumbnailHeight * aspectRatio);
            }
            else if (aspectRatio > targetAspectRatio)
            {
                thumbnailHeight = (int)(thumbnailWidth / aspectRatio);
            }

            var offset = new Point(0, 0);
            if (thumbnailWidth != imageSize)
            {
                offset.X = ((imageSize - thumbnailWidth) / 2);
            }
            if (thumbnailHeight != imageSize)
            {
                offset.Y = ((imageSize - thumbnailHeight) / 2);
            }

            var bmpImage = new Bitmap(imageSize, imageSize, PixelFormat.Format32bppArgb);
            bmpImage.SetResolution(dpi, dpi);

            using (Graphics graphics = Graphics.FromImage(bmpImage))
            {
                graphics.Clear(backgroundColor);
                graphics.InterpolationMode = interpolation;
                graphics.DrawImage(sourceImage, new Rectangle(offset.X, offset.Y, thumbnailWidth, thumbnailHeight), new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), GraphicsUnit.Pixel);
            }

            return bmpImage;
        }
    }
}
