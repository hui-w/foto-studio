using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace QLike.Foto.Common
{
    public enum ThumbType
    {
        InnerOriginal,
        StretchedFill,
        OutterOriginal,
        OutterLeftCorpped,
        OutterMiddleCorpped,
        OutterRightCorpped
    }

    public class ThumbGenerator
    {

        public static Bitmap CreateThumb(FileInfo file, Size containerSize, ThumbType type, out Size originalSize, out Size thumbSize)
        {
            using (Image source = Image.FromFile(file.FullName))
            {
                if (source.Width > containerSize.Width || source.Height > containerSize.Height)
                {
                    //Create the thumb image
                    double ratioWidth = (double)containerSize.Width / (double)source.Width;
                    double ratioHeight = (double)containerSize.Height / (double)source.Height;
                    double ratio = ratioWidth < ratioHeight ? ratioWidth : ratioHeight;
                    int thumbWidth = (int)((double)source.Width * ratio);
                    int thumbHeight = (int)((double)source.Height * ratio);

                    //If width or height is 0
                    thumbWidth = thumbWidth == 0 ? 1 : thumbWidth;
                    thumbHeight = thumbHeight == 0 ? 1 : thumbHeight;

                    Bitmap thumb = new Bitmap(thumbWidth, thumbHeight);
                    using (Graphics g = Graphics.FromImage(thumb))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.FillRectangle(Brushes.White, 0, 0, thumbWidth, thumbHeight);
                        g.DrawImage(source, 0, 0, thumbWidth, thumbHeight);
                    }

                    originalSize = source.Size;
                    thumbSize = thumb.Size;
                    return thumb;
                }
            }

            originalSize = new Size(0, 0);
            thumbSize = new Size(0, 0);
            return null;
        }

        #region PrepareThumb()
        /// <summary>
        /// Prepare the thumb for images
        /// </summary>
        /// <returns></returns>
        public static void PrepareThumb(FileInfo file, out Size sizeImage, out Size sizeThumb)
        {
            sizeImage = new Size(0, 0);
            sizeThumb = sizeImage;

            if (!file.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)) return;

            //Configure JPEG Compression Engine   
            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 70;
            System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            System.Drawing.Imaging.ImageCodecInfo[] arrayICI = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            System.Drawing.Imaging.ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];
                    break;
                }
            }

            using (System.Drawing.Image source = System.Drawing.Image.FromFile(file.FullName))
            {
                sizeImage = source.Size;

                //Thumb
                int defaultThumbWidth = 120;
                int defaultThumbHeight = 80;
                if (source.Width > defaultThumbWidth || source.Height > defaultThumbHeight)
                {
                    //Create the thumb image
                    double dW = (double)defaultThumbWidth / (double)source.Width;
                    double dH = (double)defaultThumbHeight / (double)source.Height;
                    double dWH = dW < dH ? dW : dH;
                    int width = (int)((double)source.Width * dWH);
                    int height = (int)((double)source.Height * dWH);

                    //If width or height is 0
                    width = width == 0 ? 1 : width;
                    height = height == 0 ? 1 : height;

                    // original code that creates lousy thumbnails   
                    // System.Drawing.Image ret = source.GetThumbnailImage(wi,hi,null,IntPtr.Zero);   
                    using (System.Drawing.Bitmap thumb = new Bitmap(width, height))
                    {
                        using (Graphics g = Graphics.FromImage(thumb))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.FillRectangle(Brushes.White, 0, 0, width, height);
                            g.DrawImage(source, 0, 0, width, height);
                        }
                        sizeThumb = thumb.Size;
                        string targetPath = ThumbGenerator.GetThumbPathName(file.FullName, file.Extension);
                        thumb.Save(targetPath, jpegICI, encoderParams);
                    }

                    //using (System.Drawing.Image thumb = image.GetThumbnailImage(width, height, null, IntPtr.Zero))
                    //{
                    //    sizeThumb = thumb.Size;

                    //    string newFile = Common.GetThumbPathName(Common.AbsoluteToRelative(file.FullName, true), file.Extension);
                    //    thumb.Save(newFile);
                    //}
                }
            }
        }
        #endregion

        //Get the path and name for thumb
        public static string GetThumbPathName(string fullName, string extension)
        {
            string cacheFolder = string.Concat(System.Windows.Forms.Application.StartupPath, "\\thumb\\");
            if (!Directory.Exists(cacheFolder))
            {
                Directory.CreateDirectory(cacheFolder);
            }
            return string.Concat(cacheFolder, Base64.Encode(fullName), extension);
        }
    }//end of class
}
