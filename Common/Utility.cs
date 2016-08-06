using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;

namespace QLike.Foto.Common
{
    public class Utility
    {
        /// <summary>
        /// Get the filled thumb for image source
        /// </summary>
        /// <param name="imageSource"></param>
        /// <param name="containerSize"></param>
        /// <param name="corp"></param>
        /// <param name="thumbSize"></param>
        /// <returns></returns>
        public static ImageSource GetFilledThumb(string imageSource, Size containerSize, Rotation rotation, bool corp, out Size thumbSize)
        {
            //Load the file
            BitmapImage bmp = new BitmapImage(new Uri(imageSource));
            double ratioWidth, ratioHeight, ratio;
            Size resizeToSize;
            if (rotation == Rotation.Rotate0 || rotation == Rotation.Rotate180)
            {
                ratioWidth = containerSize.Width / bmp.Width;
                ratioHeight = containerSize.Height / bmp.Height;
            }
            else
            {
                ratioWidth = containerSize.Width / bmp.Height;
                ratioHeight = containerSize.Height / bmp.Width;
            }
            ratio = ratioWidth > ratioHeight ? ratioWidth : ratioHeight;
            resizeToSize = new Size(bmp.Width * ratio, bmp.Height * ratio);

            //get the new bitmap for display
            BitmapImage resizedBmp = new BitmapImage();
            resizedBmp.BeginInit();
            resizedBmp.CacheOption = BitmapCacheOption.None;
            resizedBmp.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            resizedBmp.DecodePixelWidth = (int)Math.Ceiling(resizeToSize.Width);
            resizedBmp.DecodePixelHeight = (int)Math.Ceiling(resizeToSize.Height);
            resizedBmp.UriSource = bmp.UriSource;
            resizedBmp.Rotation = rotation;
            resizedBmp.EndInit();

            if (!corp)
            {
                //Not corpped, for mouse dragging
                if (rotation == Rotation.Rotate0 || rotation == Rotation.Rotate180)
                {
                    thumbSize = resizeToSize;
                }
                else
                {
                    thumbSize = new Size(resizeToSize.Height, resizeToSize.Width);
                }
                return resizedBmp;
            }
            else
            {
                //Corp Image
                thumbSize = containerSize;
                CroppedBitmap corppedBmp;
                Int32Rect rect;
                if (ratioWidth > ratioHeight)
                {
                    //corp height
                    if (rotation == Rotation.Rotate0 || rotation == Rotation.Rotate180)
                    {
                        rect = new Int32Rect(0, (int)((resizedBmp.PixelHeight - containerSize.Height) / 2), (int)containerSize.Width, (int)containerSize.Height);
                    }
                    else
                    {
                        rect = new Int32Rect((int)((resizedBmp.PixelWidth - containerSize.Width) / 2), 0, (int)containerSize.Width, (int)containerSize.Height);
                    }
                }
                else
                {
                    //corp width
                    if (rotation == Rotation.Rotate0 || rotation == Rotation.Rotate180)
                    {
                        rect = new Int32Rect((int)((resizedBmp.PixelWidth - containerSize.Width) / 2), 0, (int)containerSize.Width, (int)containerSize.Height);
                    }
                    else
                    {
                        rect = new Int32Rect(0, (int)((resizedBmp.PixelHeight - containerSize.Height) / 2), (int)containerSize.Width, (int)containerSize.Height);
                    }
                }

                corppedBmp = new CroppedBitmap(resizedBmp, rect);

                //set the image position
                return corppedBmp;
            }
        }

        /// <summary>
        /// Get formatted file size string
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static String FormatFileSize(Int64 fileSize)
        {
            if (fileSize < 0)
            {
                throw new ArgumentOutOfRangeException("fileSize");
            }
            else if (fileSize >= 1024 * 1024 * 1024)
            {
                return string.Format("{0:########0.00} GB", ((Double)fileSize) / (1024 * 1024 * 1024));
            }
            else if (fileSize >= 1024 * 1024)
            {
                return string.Format("{0:####0.00} MB", ((Double)fileSize) / (1024 * 1024));
            }
            else if (fileSize >= 1024)
            {
                return string.Format("{0:####0.00} KB", ((Double)fileSize) / 1024);
            }
            else
            {
                return string.Format("{0} bytes", fileSize);
            }
        }

        public static DrawingBrush GetBackgroundForEmpty()
        {
            //Background
            RectangleGeometry rect1 = new RectangleGeometry(new Rect(5, 0, 5, 5));
            RectangleGeometry rect2 = new RectangleGeometry(new Rect(0, 5, 5, 5));
            PathGeometry combin = Geometry.Combine(rect1, rect2, GeometryCombineMode.Xor, null);

            GeometryDrawing drawing = new GeometryDrawing(Brushes.LightGray, null, combin);
            DrawingBrush background = new DrawingBrush(drawing);
            background.ViewportUnits = BrushMappingMode.Absolute;
            background.Viewport = new Rect(0, 0, 16, 16);
            background.TileMode = TileMode.Tile;
            return background;
        }
    }//end of class
}
