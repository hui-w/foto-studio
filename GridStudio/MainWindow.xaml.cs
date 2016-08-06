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
using System.Windows.Shapes;
using QLike.Foto.Grid;
using QLike.Foto.GridStudio.Controls;
using System.IO;
using QLike.AutoUpdate;
using QLike.Foto.Common;

namespace QLike.Foto.GridStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Image info for grid switching
        /// </summary>
        struct ImageInfo
        {
            internal string Source;
            internal Rotation Rotation;
        }

        private FileInfo selectedGridDifinition = null;
        private Canvas gridCanvas = null;
        private List<ImageInfo> savedImageInfoList = null;

        private DefinitionList definitionList = null;
        private FolderBrowser folderBrowser = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Window_Loaded()
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            definitionList = new DefinitionList();
            this.gridTools.Children.Add(definitionList);
            definitionList.onSelectionChanged += new DefinitionList.SelectionChangedHandler(definitionList_onSelectionChanged);
            this.btnGrid.IsChecked = true;
            this.menuGrid.IsChecked = true;

            //Background
            this.viewMain.Background = Utility.GetBackgroundForEmpty();
        }
        #endregion

        #region definitionList_onSelectionChanged()
        void definitionList_onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SaveImageSources();   //save the current file list before refreshing
            this.selectedGridDifinition = e.AddedItems[0] as FileInfo;
            this.ShowGrid();
        }
        #endregion

        #region SaveFileNames()
        /// <summary>
        /// Save the selected files of current FotoGrid
        /// </summary>
        private void SaveImageSources()
        {
            if (this.gridCanvas == null)
            {
                //Grid not loaded
                return;
            }

            //Get files of current grids
            List<ImageInfo> currentImageInfoList = new List<ImageInfo>();
            int cellCount = 0;
            for (int i = 0; i < this.gridCanvas.Children.Count; i++ )
            {
                ImageCell cell = this.gridCanvas.Children[i] as ImageCell;
                if (cell != null)
                {
                    cellCount++;    //count of the cell
                    if (!string.IsNullOrEmpty(cell.ImageSource))
                    {
                        currentImageInfoList.Add(new ImageInfo() { Source = cell.ImageSource, Rotation = cell.ImageRotation });    //selected files
                    }
                }
            }

            if (this.savedImageInfoList == null)
            {
                //First time to save
                this.savedImageInfoList = currentImageInfoList;
            }
            else
            {
                if (this.savedImageInfoList.Count > cellCount)
                {
                    //Not all files in the old list are shown to current grid
                    this.savedImageInfoList.RemoveRange(0, cellCount);
                    this.savedImageInfoList.InsertRange(0, currentImageInfoList);
                }
                else
                {
                    this.savedImageInfoList = currentImageInfoList;
                }
            }
        }
        #endregion

        #region RestoreImageSources()
        /// <summary>
        /// Restore the source of iamge cells
        /// </summary>
        private void RestoreImageSources()
        {
            if (this.savedImageInfoList != null)
            {
                for (int i = 0; i < this.gridCanvas.Children.Count; i++)
                {
                    if (i >= this.savedImageInfoList.Count)
                    {
                        break;
                    }
                    ImageCell cell = this.gridCanvas.Children[i] as ImageCell;
                    if (cell != null)
                    {
                        cell.OpenImage(this.savedImageInfoList[i].Source, this.savedImageInfoList[i].Rotation);
                    }
                }
            }
        }
        #endregion

        #region ShowGrid()
        /// <summary>
        /// Show the grid, and load iamges from saved file names
        /// </summary>
        private void ShowGrid()
        {
            if (this.selectedGridDifinition == null)
            {
                return;
            }

            int width, height;
            if (!Int32.TryParse(this.txtWidth.Text, out width) || width < 100 || width > 1000)
            {
                width = 400;
            }
            if (!Int32.TryParse(this.txtHeight.Text, out height) || height < 100 || height > 1000)
            {
                height = 500;
            }
            this.txtWidth.Text = width.ToString();
            this.txtHeight.Text = height.ToString();

            this.lblSelectedFilePath.Content = this.selectedGridDifinition.Name;

            try
            {
                using (FotoGrid grid = new FotoGrid(this.selectedGridDifinition.FullName, width, height))
                {
                    this.gridCanvas = new Canvas();
                    this.gridCanvas.Background = Brushes.WhiteSmoke;
                    this.gridCanvas.Width = width;
                    this.gridCanvas.Height = height;

                    //cells
                    foreach (Cell cell in grid.Cells)
                    {
                        ImageCell imgCell = new ImageCell(cell.Width, cell.Height, cell.Left, cell.Top, grid.CellSpacing);

                        this.gridCanvas.Children.Add(imgCell); ;
                    }

                    this.viewMain.Content = this.gridCanvas;

                    //gray background
                    this.viewMain.Background = Brushes.LightGray;
                }

                //Restore the image sources
                this.RestoreImageSources();
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }
        #endregion

        #region ShowError()
        private void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region Menu Item Events
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Header.ToString() == "_Exit")
            {
                this.Close();
            }
            else if (((MenuItem)sender).Header.ToString() == "_About...")
            {
                AboutWindow wnd = new AboutWindow();
                wnd.Owner = this;
                wnd.ShowDialog();
            }
            else if (((MenuItem)sender).Header.ToString() == "_Check Update...")
            {
                Updater up = new Updater(false, true);
                up.onUpdateCompleted += new Updater.UpdateCompletedHandler(up_onUpdateCompleted);
                up.Update();
            }
            else if (((MenuItem)sender).Header.ToString() == "_Grid List")
            {
                this.btnGrid_MouseLeftButtonUp(this, null);
            }
            else if (((MenuItem)sender).Header.ToString() == "_Folder View")
            {
                this.btnFolder_MouseLeftButtonUp(this, null);
            }
        }

        void up_onUpdateCompleted(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.SaveImageSources();   //save the current file list before refreshing
            this.ShowGrid();
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            if (this.gridCanvas == null)
            {
                this.ShowError("No Photo Grid is Loaded!");
                return;
            }
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            dlg.Title = "Save as PNG";
            dlg.Filter = "PNG File(.png）|*.jpg";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.PublishToPng(new Uri(dlg.FileName), this.gridCanvas);
            }
        }
        #endregion

        #region Save: PublishToPng() viewMain_SizeChanged()
        public void PublishToPng(Uri path, Canvas surface)
        {
            if (path == null) return;

            Transform transform = surface.LayoutTransform;
            surface.LayoutTransform = null;

            Size size = new Size(surface.Width, surface.Height);
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap =
            new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(outStream);
            }
            surface.LayoutTransform = transform;

            //reset the position of canvas
            surface.SetValue(Canvas.MarginProperty, 
                new Thickness((this.viewMain.ActualWidth - surface.Width) / 2,(this.viewMain.ActualHeight - surface.Height) / 2,
                    0,0));
        }

        private void viewMain_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //resotre the position of the foto grid canvas, to make sure it's in the center
            if (this.gridCanvas != null && (this.gridCanvas.Margin.Left > 0 || this.gridCanvas.Margin.Top > 0))
            {
                this.gridCanvas.Margin = new Thickness(0, 0, 0, 0);
            }
        }
        #endregion

        #region Window_Closing()
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveImageSources();
            if (this.savedImageInfoList != null && this.savedImageInfoList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to exit?", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region ucThumnContainer_onMessage()
        void ucThumnContainer_onMessage(object sender, string e)
        {
            this.lblSelectedFilePath.Content = e;
        }
        #endregion

        #region Switch Buttons: btnGrid_MouseLeftButtonUp() btnFolder_MouseLeftButtonUp()
        private void btnGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.btnGrid.IsChecked)
            {
                this.btnGrid.IsChecked = true;
                this.btnFolder.IsChecked = false;

                this.menuGrid.IsChecked = true;
                this.menuFolder.IsChecked = false;

                this.definitionList.Visibility = Visibility.Visible;
                this.folderBrowser.Visibility = Visibility.Hidden;
                this.toolColumn.Width = new GridLength(160);
            }
        }

        private void btnFolder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.btnFolder.IsChecked)
            {
                this.btnGrid.IsChecked = false;
                this.btnFolder.IsChecked = true;

                this.menuGrid.IsChecked = false;
                this.menuFolder.IsChecked = true;

                if (this.folderBrowser == null)
                {
                    this.folderBrowser = new FolderBrowser();
                    this.folderBrowser.ucThumnContainer.onMessage += new ThumbContainer.MessageHandler(ucThumnContainer_onMessage);
                    this.gridTools.Children.Add(folderBrowser);
                }
                else
                {
                    this.folderBrowser.Visibility = Visibility.Visible;
                }
                this.definitionList.Visibility = Visibility.Hidden;
                this.toolColumn.Width = new GridLength(380);
            }
        }
        #endregion
    }//end of class
}
