using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for FolderBrowser.xaml
    /// </summary>
    public partial class FolderBrowser : UserControl
    {
        public FolderBrowser()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = double.NaN;
            this.Height = double.NaN;
            this.LoadDrives();
        }

        private void LoadDrives()
        {
            var directory = new ObservableCollection<DirectoryRecord>();

            foreach (var drive in DriveInfo.GetDrives())
            {
                directory.Add(
                    new DirectoryRecord
                    {
                        Info = new DirectoryInfo(drive.RootDirectory.FullName)
                    }
                );
            }

            treeDirectory.ItemsSource = directory;
        }
    }//end of class
}
