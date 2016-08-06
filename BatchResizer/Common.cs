using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace QLike.Foto.BatchResizer
{
    internal class Common
    {
        public static void PrepareFolder(string path)
        {
            string currentPathWithoutSlash = path.Substring(0, path.LastIndexOf("\\"));
            if (!Directory.Exists(currentPathWithoutSlash))
            {
                string parentPathWithoutSlash = currentPathWithoutSlash.Substring(0, currentPathWithoutSlash.LastIndexOf("\\"));
                PrepareFolder(parentPathWithoutSlash);
                Directory.CreateDirectory(currentPathWithoutSlash);
            }
        }

        public static bool IsImage(FileInfo file)
        {
            if (
                    file.Extension.ToLower() == ".gif" ||
                    file.Extension.ToLower() == ".jpg" ||
                    file.Extension.ToLower() == ".jpeg" ||
                    file.Extension.ToLower() == ".png"
                    )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create the thumb
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="type"></param>
        /// <param name="targetFolder"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <param name="jpegQuality"></param>
        /// <param name="overwrite"></param>
        /// <param name="newSize"></param>
        /// <returns>-1: not photo; 0: smaller than targer; 1: normal; 2: skipped for not overwriting</returns>
        public static int CreateThumb(FileInfo sourceFile, ThumbType type, string targetFolder, int targetWidth, int targetHeight, int jpegQuality, bool overwrite, out Size newSize)
        {
            newSize = new Size(0, 0);

            if (!Common.IsImage(sourceFile))
            {
                return -1;
            }

            string targetFile = targetFolder.EndsWith("\\") ? string.Concat(targetFolder, sourceFile.Name) : string.Concat(targetFolder, "\\", sourceFile.Name);
            if (File.Exists(targetFile) && !overwrite)
            {
                return 2;
            }

            //Configure JPEG Compression Engine   
            System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
            long[] quality = new long[1];
            quality[0] = jpegQuality;
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

            //Create thumb
            using (Image source = Image.FromFile(sourceFile.FullName))
            {
                Size sizeImage = source.Size;

                //Thumb
                if (source.Width > targetWidth || source.Height > targetHeight)
                {
                    //Create the thumb image
                    double dW = (double)targetWidth / (double)source.Width;
                    double dH = (double)targetHeight / (double)source.Height;
                    double dWH;
                    int width, height;
                    if (type == ThumbType.InsideUniform)
                    {
                        dWH = dW < dH ? dW : dH;
                    }
                    else
                    {
                        dWH = dW > dH ? dW : dH;
                    }
                    width = (int)((double)source.Width * dWH);
                    height = (int)((double)source.Height * dWH);

                    //If width or height is 0
                    width = width == 0 ? 1 : width;
                    height = height == 0 ? 1 : height;

                    // original code that creates lousy thumbnails   
                    // System.Drawing.Image ret = source.GetThumbnailImage(wi,hi,null,IntPtr.Zero); 
                    if (type == ThumbType.CorpToFill)
                    {
                        using (System.Drawing.Bitmap thumb = new Bitmap(targetWidth, targetHeight))
                        {
                            using (Graphics g = Graphics.FromImage(thumb))
                            {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                //g.FillRectangle(Brushes.White, 0, 0, width, height);
                                if (width > targetWidth)
                                {
                                    g.DrawImage(source, (targetWidth - width) / 2, 0, width, height);
                                }
                                else if (height > targetHeight)
                                {
                                    g.DrawImage(source, 0, (targetHeight - height) / 2, width, height);
                                }
                                else
                                {
                                    g.DrawImage(source, 0, 0, width, height);
                                }
                            }
                            newSize = thumb.Size;

                            thumb.Save(targetFile, jpegICI, encoderParams);
                        }
                    }
                    else
                    {
                        using (System.Drawing.Bitmap thumb = new Bitmap(width, height))
                        {
                            using (Graphics g = Graphics.FromImage(thumb))
                            {
                                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                //g.FillRectangle(Brushes.White, 0, 0, width, height);
                                g.DrawImage(source, 0, 0, width, height);
                            }
                            newSize = thumb.Size;

                            thumb.Save(targetFile, jpegICI, encoderParams);
                        }
                    }

                    return 1;

                    //using (System.Drawing.Image thumb = image.GetThumbnailImage(width, height, null, IntPtr.Zero))
                    //{
                    //    sizeThumb = thumb.Size;

                    //    string newFile = Common.GetThumbPathName(Common.AbsoluteToRelative(file.FullName, true), file.Extension);
                    //    thumb.Save(newFile);
                    //}
                }
                else
                {
                    //Not necessary to create thumb
                    return 0;
                }
            }
        }
    }//end of class
}
