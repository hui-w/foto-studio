using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QLike.Foto.GridStudio.Controls
{
    internal class DirectoryRecord
    {
        public DirectoryInfo Info { get; set; }

        public IEnumerable<FileInfo> Files
        {
            get
            {
                return from file in Info.GetFiles()
                       where IsImage(file)
                       select file;
            }
        }

        public IEnumerable<DirectoryRecord> Directories
        {
            get
            {
                return from di in Info.GetDirectories("*", SearchOption.TopDirectoryOnly)
                       where (di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden 
                       select new DirectoryRecord { Info = di };
            }
        }

        private bool IsImage(FileInfo file)
        {
            if (file.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || file.Extension.Equals(".gif", StringComparison.OrdinalIgnoreCase)
                || file.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }//end if class
}
