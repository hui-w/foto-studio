using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QLike.AutoUpdate
{
    public class Common
    {
        private static readonly string ClientConfigFileName = "Version.xml";
        public static readonly string ServerConfigFileName = "Version.xml";

        public static string ClientFolder
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static string ClientConfigFile
        {
            get
            {
                return Common.CombinePath(Common.ClientFolder, Common.ClientConfigFileName, false);
            }
        }

        public static string GetServerSideUrl(string serverBase, string fileName)
        {
            return string.Concat(Common.CombinePath(serverBase, fileName, true), "?timestamp=", DateTime.Now.Ticks.ToString());
        }

        public static void PrepareClientFolder(string path)
        {
            string currentPathWithoutSlash = path.Substring(0, path.LastIndexOf("\\"));
            if (!Directory.Exists(currentPathWithoutSlash))
            {
                string parentPathWithoutSlash = currentPathWithoutSlash.Substring(0, currentPathWithoutSlash.LastIndexOf("\\"));
                PrepareClientFolder(parentPathWithoutSlash);
                Directory.CreateDirectory(currentPathWithoutSlash);
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

        public static string CombinePath(string path, string name, bool httpPath)
        {
            StringBuilder pathBuilder = new StringBuilder();
            string splitter = httpPath ? "/" : "\\";
            if (path.EndsWith(splitter))
            {
                pathBuilder.Append(path.Substring(0, path.Length - 1));
            }
            else
            {
                pathBuilder.Append(path);
            }
            pathBuilder.Append(splitter);
            if (name.StartsWith(splitter))
            {
                pathBuilder.Append(name.Substring(1));
            }
            else
            {
                pathBuilder.Append(name);
            }

            return pathBuilder.ToString();
        }
    }//end of class
}
