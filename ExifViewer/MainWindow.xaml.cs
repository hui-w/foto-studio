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
using QLike.Foto.Common;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Threading;

namespace QLike.Foto.ExifViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string selectedFilePathName = string.Empty;
        private string selectedFilePath = string.Empty;
        private string[] filesInFolder = null;

        #region MainWindow()
        public MainWindow()
        {
            InitializeComponent();

            //Shortcuts
            this.CommandBindings.Add
                (new CommandBinding
                    (CustomCommands.FirstCommand,
                    (sender, e) =>
                    {
                        this.ShowFirst();
                    },
                    (sender, e) =>
                    { e.CanExecute = true; }
                    )
                );

            this.CommandBindings.Add
                (new CommandBinding
                    (CustomCommands.PrevCommand,
                    (sender, e) =>
                    {
                        this.ShowPrev();
                    },
                    (sender, e) =>
                    { e.CanExecute = true; }
                    )
                );

            this.CommandBindings.Add
                (new CommandBinding
                    (CustomCommands.NextCommand,
                    (sender, e) =>
                    {
                        this.ShowNext();
                    },
                    (sender, e) =>
                    { e.CanExecute = true; }
                    )
                );

            this.CommandBindings.Add
                (new CommandBinding
                    (CustomCommands.LastCommand,
                    (sender, e) =>
                    {
                        this.ShowLast();
                    },
                    (sender, e) =>
                    { e.CanExecute = true; }
                    )
                );

            //System Shortcuts
            this.CommandBindings.Add(new CommandBinding
                (ApplicationCommands.Help,
                (sender, e) =>
                {
                    AboutWindow wnd = new AboutWindow();
                    wnd.Owner = this;
                    wnd.ShowDialog();
                },
                (sender, e) =>
                { e.CanExecute = true; }
                 )
             );

            this.CommandBindings.Add(new CommandBinding
                (ApplicationCommands.Open,
                (sender, e) =>
                {
                    System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
                    dlg.Title = "Open Image";
                    dlg.Filter = "JPG File(.jpg）|*.jpg|All Files|*.*";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.OpenImage(dlg.FileName);
                    }
                },
                (sender, e) =>
                { e.CanExecute = true; }
                 )
             );
        }
        #endregion

        #region Drag and Drop
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];

                foreach (string file in files)
                {
                    this.OpenImage(file);
                }
            }
        }

        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                if ((e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey)
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.Move;
                }
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
        #endregion

        #region Window_Loaded()
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.imgBorder.Background = Utility.GetBackgroundForEmpty();
            this.SetNavigatorStatus();

            HotKey hotKeyWindowState = new HotKey(this, HotKey.KeyFlags.MOD_CONTROL | HotKey.KeyFlags.MOD_SHIFT, System.Windows.Forms.Keys.F12);
            hotKeyWindowState.OnHotKey += new HotKey.OnHotKeyEventHandler(delegate
                {
                    if (this.WindowState == WindowState.Normal)
                    {
                        this.WindowState = WindowState.Minimized;
                        this.Hide();
                    }
                    else
                    {
                        this.Show();
                        this.WindowState = WindowState.Normal;
                    }
                });

            HotKey hotKeyHome = new HotKey(this, HotKey.KeyFlags.NONE, System.Windows.Forms.Keys.Home);
            hotKeyHome.OnHotKey += new HotKey.OnHotKeyEventHandler(delegate
            {
                this.ShowFirst();
            });

            HotKey hotKeyPrevious = new HotKey(this, HotKey.KeyFlags.NONE, System.Windows.Forms.Keys.PageUp);
            hotKeyPrevious.OnHotKey += new HotKey.OnHotKeyEventHandler(delegate
            {
                this.ShowPrev();
            });

            HotKey hotKeyNext = new HotKey(this, HotKey.KeyFlags.NONE, System.Windows.Forms.Keys.PageDown);
            hotKeyNext.OnHotKey += new HotKey.OnHotKeyEventHandler(delegate
            {
                this.ShowNext();
            });

            HotKey hotKeyEnd = new HotKey(this, HotKey.KeyFlags.NONE, System.Windows.Forms.Keys.End);
            hotKeyEnd.OnHotKey += new HotKey.OnHotKeyEventHandler(delegate
            {
                this.ShowLast();
            });
        }
        #endregion

        #region SetWindowTitle()
        private void SetWindowTitle(string fileName)
        {
            if (this.Dispatcher.CheckAccess())
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    this.Title = "Exif Viewer";
                }
                else
                {
                    this.Title = string.Format("Exif Viewer - {0}", fileName);
                }
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.SetWindowTitle(fileName);
                });
            }
        }
        #endregion

        #region MenuEvents
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Header.ToString() == "_Exit")
            {
                this.Close();
            }
        }

        private void menuExif_Click(object sender, RoutedEventArgs e)
        {
            this.ShowExif(!this.menuExif.IsChecked);
        }
        #endregion

        #region Navigator
        private void SetNavigatorStatus()
        {
            int currentIndex = this.GetCurrentIndex();
            if (currentIndex == -1)
            {
                this.menuFirst.IsEnabled = false;
                this.menuPrev.IsEnabled = false;
                this.menuNext.IsEnabled = false;
                this.menuLast.IsEnabled = false;
                this.btnFirst.IsEnabled = false;
                this.btnPrev.IsEnabled = false;
                this.btnNext.IsEnabled = false;
                this.btnLast.IsEnabled = false;
                this.txtNavigation.Text = "0 of 0";
                this.lblStatus.Content = "Ready";
            }
            else
            {
                this.menuFirst.IsEnabled = true;
                this.menuPrev.IsEnabled = true;
                this.menuNext.IsEnabled = true;
                this.menuLast.IsEnabled = true;
                this.btnFirst.IsEnabled = true;
                this.btnPrev.IsEnabled = true;
                this.btnNext.IsEnabled = true;
                this.btnLast.IsEnabled = true;
                this.txtNavigation.Text = string.Format("{0} of {1}", currentIndex + 1, this.filesInFolder.Count());
                this.lblStatus.Content = string.Format("{0} of {1} photos in {2}", currentIndex + 1, this.filesInFolder.Count(), this.selectedFilePath);

                if (currentIndex == 0)
                {
                    this.menuFirst.IsEnabled = false;
                    this.menuPrev.IsEnabled = false;
                    this.btnFirst.IsEnabled = false;
                    this.btnPrev.IsEnabled = false;
                }

                if (currentIndex == this.filesInFolder.Length - 1)
                {
                    this.menuNext.IsEnabled = false;
                    this.menuLast.IsEnabled = false;
                    this.btnNext.IsEnabled = false;
                    this.btnLast.IsEnabled = false;
                }
            }
        }

        private int GetCurrentIndex()
        {
            if (this.filesInFolder == null)
            {
                //not loaded
                return -1;
            }

            for (int i = 0; i < this.filesInFolder.Length; i++)
            {
                if (this.selectedFilePathName.Equals(this.filesInFolder[i], StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            //not found
            return -1;
        }

        private void ShowFirst()
        {
            int currentIndex = this.GetCurrentIndex();
            if (currentIndex != -1 && currentIndex != 0)
            {
                this.OpenImage(this.filesInFolder[0]);
            }
        }

        private void ShowPrev()
        {
            int currentIndex = this.GetCurrentIndex();
            if (currentIndex != -1 && currentIndex != 0)
            {
                this.OpenImage(this.filesInFolder[currentIndex - 1]);
            }
        }

        private void ShowNext()
        {
            int currentIndex = this.GetCurrentIndex();
            if (currentIndex != -1 && currentIndex < this.filesInFolder.Length - 1)
            {
                this.OpenImage(this.filesInFolder[currentIndex + 1]);
            }
        }

        private void ShowLast()
        {
            int currentIndex = this.GetCurrentIndex();
            if (currentIndex != -1 && currentIndex != this.filesInFolder.Length - 1)
            {
                this.OpenImage(this.filesInFolder[this.filesInFolder.Length - 1]);
            }
        }
        #endregion

        #region OpenImage()
        private void OpenImage(string filePathName)
        {
            if (!filePathName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            this.txtLoading.Visibility = Visibility.Visible;

            Thread thread = new Thread(new ParameterizedThreadStart(
                (obj) => {
                    //Show file name and path
                    this.selectedFilePathName = obj.ToString();
                    string fileName = filePathName.Substring(filePathName.LastIndexOf("\\") + 1);
                    this.SetWindowTitle(fileName);

                    string filePath = filePathName.Substring(0, filePathName.LastIndexOf("\\"));
                    if (!filePath.Equals(this.selectedFilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        this.selectedFilePath = filePath;
                        DirectoryInfo dir = new DirectoryInfo(this.selectedFilePath);
                        IEnumerable<string> files = from f in dir.GetFiles()
                                                    where f.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                                                    select f.FullName;
                        this.filesInFolder = files.ToArray<string>();
                    }

                    //Load the image
                    this.LoadImage(filePathName);
                }
                ));
            thread.Start(filePathName);
            
        }

        private void LoadImage(string filePathName)
        {
            if (this.Dispatcher.CheckAccess())
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(filePathName));
                this.imgPreview.Source = bitmapImage;

                //Set button status
                this.SetNavigatorStatus();

                //Load and show the exif info
                this.LoadExif(filePathName);
                this.ShowExif(true);

                this.txtLoading.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.LoadImage(filePathName);
                });
            }
        }
        #endregion

        #region LoadExif()
        private void LoadExif(string file)
        {
            ExifManager exif = new ExifManager(file);
            bool noExif = exif.ResolutionX == 0;

            PropertyGrid exifGrid = new PropertyGrid();
            exifGrid.HeaderText = file.Substring(file.LastIndexOf("\\") + 1);
            exifGrid.ExifDict.Add("Image Info:", "NULL");
            exifGrid.ExifDict.Add("Width", string.Format("{0}px", exif.Width));
            exifGrid.ExifDict.Add("Height", string.Format("{0}px", exif.Height));
            exifGrid.ExifDict.Add("Resolution", noExif ? string.Empty : string.Format("{0}*{1} dpi", exif.ResolutionX, exif.ResolutionY));
            exifGrid.ExifDict.Add("Orientation", noExif ? string.Empty : Enum.GetName(typeof(Orientations), exif.Orientation));

            exifGrid.ExifDict.Add("Copyright:", noExif ? string.Empty : exif.Title);
            exifGrid.ExifDict.Add("Description", noExif ? string.Empty : exif.Description);
            exifGrid.ExifDict.Add("Copyright", noExif ? string.Empty : exif.Copyright);

            exifGrid.ExifDict.Add("Equipment:", "NULL");
            exifGrid.ExifDict.Add("Maker", noExif ? string.Empty : exif.EquipmentMaker);
            exifGrid.ExifDict.Add("Model", noExif ? string.Empty : exif.EquipmentModel);
            exifGrid.ExifDict.Add("Software", noExif ? string.Empty : exif.Software);

            exifGrid.ExifDict.Add("Date and time:", "NULL");
            exifGrid.ExifDict.Add("General", noExif ? string.Empty : this.FormatDateTime(exif.DateTimeLastModified));
            exifGrid.ExifDict.Add("Original", noExif ? string.Empty : this.FormatDateTime(exif.DateTimeOriginal));
            exifGrid.ExifDict.Add("Digitized", noExif ? string.Empty : this.FormatDateTime(exif.DateTimeDigitized));

            exifGrid.ExifDict.Add("Shooting conditions:", "NULL");
            exifGrid.ExifDict.Add("Exposure time", noExif ? string.Empty : string.Format("{0}s", exif.ExposureTime.ToString("N4")));
            exifGrid.ExifDict.Add("Exposure program", noExif ? string.Empty : Enum.GetName(typeof(ExposurePrograms), exif.ExposureProgram));
            exifGrid.ExifDict.Add("Exposure mode", noExif ? string.Empty : Enum.GetName(typeof(ExposureMeteringModes), exif.ExposureMeteringMode));
            exifGrid.ExifDict.Add("Aperture", noExif ? string.Empty : string.Format("F{0}", exif.Aperture.ToString("N2")));
            exifGrid.ExifDict.Add("ISO", noExif ? string.Empty : exif.ISO.ToString());
            exifGrid.ExifDict.Add("Subject distance", noExif ? string.Empty : string.Format("{0}m", exif.SubjectDistance.ToString("N2")));
            exifGrid.ExifDict.Add("Focal length", noExif ? string.Empty : exif.FocalLength.ToString());
            exifGrid.ExifDict.Add("Flash", noExif ? string.Empty : Enum.GetName(typeof(FlashModes), exif.FlashMode));
            exifGrid.ExifDict.Add("Light source (WB)", noExif ? string.Empty : Enum.GetName(typeof(LightSources), exif.LightSource));

            this.exifContainer.Content = exifGrid;
        }
        #endregion

        #region ShowExif()
        private void ShowExif(bool show)
        {
            if (show)
            {
                if (this.propertyColumn.ActualWidth <= 0)
                {
                    this.propertyColumn.Width = new GridLength(260, GridUnitType.Pixel);
                }
                this.menuExif.IsChecked = true;
            }
            else
            {
                this.propertyColumn.Width = new GridLength(0, GridUnitType.Pixel);
                this.menuExif.IsChecked = false;
            }
        }
        #endregion

        #region FormatDateTime()
        private string FormatDateTime(DateTime time)
        {
            if (!time.Equals(DateTime.MinValue))
            {
                return time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }//end of class

    #region class CustomCommands
    public static class CustomCommands
    {
        private static RoutedUICommand firstCommand;
        public static RoutedUICommand FirstCommand
        {
            get
            {
                if (firstCommand == null)
                {
                    firstCommand = new RoutedUICommand("First", "First", typeof(MainWindow), new InputGestureCollection { new KeyGesture(Key.Home, ModifierKeys.None) });
                }
                return firstCommand;
            }
        }

        private static RoutedUICommand prevCommand;
        public static RoutedUICommand PrevCommand
        {
            get
            {
                if (prevCommand == null)
                {
                    prevCommand = new RoutedUICommand("Prev", "Prev", typeof(MainWindow), new InputGestureCollection { new KeyGesture(Key.PageUp, ModifierKeys.None) });
                }
                return prevCommand;
            }
        }

        private static RoutedUICommand nextCommand;
        public static RoutedUICommand NextCommand
        {
            get
            {
                if (nextCommand == null)
                {
                    nextCommand = new RoutedUICommand("Next", "Next", typeof(MainWindow), new InputGestureCollection { new KeyGesture(Key.PageDown, ModifierKeys.None) });
                }
                return nextCommand;
            }
        }

        private static RoutedUICommand lastCommand;
        public static RoutedUICommand LastCommand
        {
            get
            {
                if (lastCommand == null)
                {
                    lastCommand = new RoutedUICommand("Last", "Last", typeof(MainWindow), new InputGestureCollection { new KeyGesture(Key.End, ModifierKeys.None) });
                }
                return lastCommand;
            }
        }
    }
    #endregion
}
