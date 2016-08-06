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
using QLike.Foto.GridStudio.Elements;
using QLike.Foto.Common;

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for ImageCell.xaml
    /// </summary>
    public partial class ImageCell : UserControl, IThumb
    {
        /// <summary>
        /// cellspacing for caculating the cornor
        /// </summary>
        private int cellSpacing = 0;

        private bool imageCaptured = false;
        private Point dragStartPoint;
        private Size resizedToSize;

        private OpenButton btnOpen = new OpenButton();
        private CloseButton btnClose = new CloseButton();
        private TurnLeftButton btnTurnLeft = new TurnLeftButton();
        private TurnRightButton btnTurnRight = new TurnRightButton();

        public string ImageSource
        {
            get;
            set;
        }

        public Rotation ImageRotation
        {
            get;
            set;
        }

        public ImageCell(int width, int height, int left, int top, int cellSpacing)
        {
            InitializeComponent();

            this.Width = width;
            this.Height = height;
            this.Margin = new Thickness() { Left = left, Top = top };
            this.cellSpacing = cellSpacing;
        }

        #region grid_Loaded()
        private void grid_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = this.grid.ActualWidth;
            double gridHeight = this.grid.ActualHeight;

            //Background and mask
            this.rect.Width = this.Width;
            this.rect.Height = this.Height;
            this.rectGeo1.Rect = new Rect(0, 0, this.Width, this.Height);
            this.rectGeo1.RadiusX = this.cellSpacing;
            this.rectGeo1.RadiusY = this.cellSpacing;

            //Image events
            this.image.MouseLeftButtonDown += new MouseButtonEventHandler(image_MouseLeftButtonDown);
            this.image.MouseLeftButtonUp += new MouseButtonEventHandler(image_MouseLeftButtonUp);
            this.image.MouseMove += new MouseEventHandler(image_MouseMove);
            this.image.MouseLeave += new MouseEventHandler(image_MouseLeave);
            this.image.MouseEnter += new MouseEventHandler(image_MouseEnter);

            //Open button
            double btnAddSize = gridWidth > gridHeight ? gridHeight / 2 : gridWidth / 2;
            this.btnOpen.Width = btnAddSize;
            this.btnOpen.Height = btnAddSize;
            this.btnOpen.Margin = new Thickness((gridWidth - this.btnOpen.Width) / 2,
                (gridHeight - this.btnOpen.Height) / 2, 0, 0);
            this.btnOpen.MouseLeftButtonUp += new MouseButtonEventHandler(btnOpen_MouseLeftButtonUp);
            this.grid.Children.Add(this.btnOpen);

            double btnSize = 20;
            double btnMargin = 2;

            //Close button
            this.btnClose.Width = btnSize;
            this.btnClose.Height = btnSize;
            this.btnClose.Margin = new Thickness(gridWidth - this.cellSpacing - btnSize, this.cellSpacing, 0, 0);
            this.btnClose.MouseLeftButtonUp += new MouseButtonEventHandler(btnClose_MouseLeftButtonUp);
            this.grid.Children.Add(this.btnClose);

            //Turn button
            this.btnTurnRight.Width = btnSize;
            this.btnTurnRight.Height = btnSize;
            this.btnTurnRight.Margin = new Thickness(gridWidth - this.cellSpacing - btnSize * 2 - btnMargin, this.cellSpacing, 0, 0);
            this.btnTurnRight.MouseLeftButtonUp += new MouseButtonEventHandler(btnTurnRight_MouseLeftButtonUp);
            this.grid.Children.Add(this.btnTurnRight);

            this.btnTurnLeft.Width = btnSize;
            this.btnTurnLeft.Height = btnSize;
            this.btnTurnLeft.Margin = new Thickness(gridWidth - this.cellSpacing - btnSize * 3 - btnMargin * 2, this.cellSpacing, 0, 0);
            this.btnTurnLeft.MouseLeftButtonUp += new MouseButtonEventHandler(btnTurnLeft_MouseLeftButtonUp);
            this.grid.Children.Add(this.btnTurnLeft);

            //Hide float buttons
            this.ShowFloatButtons(false);
        }
        #endregion

        #region Floated Tools: btnClose_MouseLeftButtonUp() btnTurnLeft_MouseLeftButtonUp() btnTurnRight_MouseLeftButtonUp()
        void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.OpenImage(string.Empty, Rotation.Rotate0);
        }

        void btnTurnLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.RotateImage(-1);
        }

        void btnTurnRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.RotateImage(1);
        }

        private void RotateImage(int direction)
        {
            if (direction == 1)
            {
                switch (this.ImageRotation)
                {
                    case Rotation.Rotate0:
                        this.OpenImage(this.ImageSource, Rotation.Rotate90);
                        break;
                    case Rotation.Rotate90:
                        this.OpenImage(this.ImageSource, Rotation.Rotate180);
                        break;
                    case Rotation.Rotate180:
                        this.OpenImage(this.ImageSource, Rotation.Rotate270);
                        break;
                    case Rotation.Rotate270:
                        this.OpenImage(this.ImageSource, Rotation.Rotate0);
                        break;
                }
            }
            else if (direction == -1)
            {
                switch (this.ImageRotation)
                {
                    case Rotation.Rotate0:
                        this.OpenImage(this.ImageSource, Rotation.Rotate270);
                        break;
                    case Rotation.Rotate270:
                        this.OpenImage(this.ImageSource, Rotation.Rotate180);
                        break;
                    case Rotation.Rotate180:
                        this.OpenImage(this.ImageSource, Rotation.Rotate90);
                        break;
                    case Rotation.Rotate90:
                        this.OpenImage(this.ImageSource, Rotation.Rotate0);
                        break;
                }
            }
        }
        #endregion

        void btnOpen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Title = "Select Image";
            dlg.Filter = "JPG File(.jpg）|*.jpg|All Files|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.OpenImage(dlg.FileName, Rotation.Rotate0);
            }
        }

        #region OpenImage()
        public void OpenImage(string imageSource, Rotation imageRotation)
        {
            this.ImageSource = imageSource;
            this.ImageRotation = imageRotation;
            if (string.IsNullOrEmpty(imageSource))
            {
                //Colse current file
                this.ShowFloatButtons(false);
                this.btnOpen.Visibility = Visibility.Visible;
                this.image.Source = null;
                return;
            }

            //Show thumb
            this.image.Source = Utility.GetFilledThumb(
                this.ImageSource, 
                new Size(this.Width, this.Height),
                imageRotation,
                false, 
                out this.resizedToSize);

            //set the image position
            double marginLeft = 0;
            double marginTop = 0;
            if (this.resizedToSize.Width >= this.Width)
            {
                marginLeft = (this.Width - this.resizedToSize.Width) / 2;
            }
            if (this.resizedToSize.Height >= this.Height)
            {
                marginTop = (this.Height - this.resizedToSize.Height) / 2;
            }
            this.image.Margin = new Thickness(marginLeft, marginTop, 0, 0);
            this.image.Width = this.resizedToSize.Width;
            this.image.Height = this.resizedToSize.Height;
            //this.image.ToolTip = dlg.FileName;

            //set the cursor
            if (this.resizedToSize.Width > this.Width)
            {
                //this.Cursor = Cursors.SizeWE;
            }
            else if (this.resizedToSize.Height > this.Height)
            {
                //this.Cursor = Cursors.SizeNS;
            }
            this.image.Cursor = Cursors.Hand;

            //Hide the open button
            this.btnOpen.Visibility = Visibility.Hidden;
        }
        #endregion

        #region MoveImage: image_MouseLeftButtonDown() image_MouseLeftButtonUp() image_MouseMove()
        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.dragStartPoint = e.GetPosition(sender as Image);
            this.imageCaptured = true;
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.imageCaptured = false;
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.imageCaptured)
            {
                Image img = sender as Image;
                Point endPoint = e.GetPosition(img);
                Thickness oldMargin = img.Margin;
                double newMarginLeft = oldMargin.Left;
                double newMarginTop = oldMargin.Top;
                if (this.resizedToSize.Width > this.Width)
                {
                    newMarginLeft += endPoint.X - dragStartPoint.X;
                    if (newMarginLeft < this.Width - this.resizedToSize.Width)
                    {
                        newMarginLeft = this.Width - this.resizedToSize.Width;
                    }
                    if (newMarginLeft > 0)
                    {
                        newMarginLeft = 0;
                    }
                }
                else if(this.resizedToSize.Height > this.Height)
                {
                    newMarginTop += endPoint.Y - dragStartPoint.Y;
                    if (newMarginTop < this.Height - this.resizedToSize.Height)
                    {
                        newMarginTop = this.Height - this.resizedToSize.Height;
                    }
                    if (newMarginTop > 0)
                    {
                        newMarginTop = 0;
                    }
                }
                img.Margin = new Thickness(newMarginLeft, newMarginTop, 0, 0);
            }
        }
        #endregion

        #region ShowHide the controller: image_MouseEnter() image_MouseLeave()
        private void image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.btnClose.Visibility != Visibility.Visible)
            {
                this.ShowFloatButtons(true);
            }
        }

        private void image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imageCaptured = false;

            //hide the change label
            Point pt = e.GetPosition(this);
            if (pt.X <= 0 || pt.Y <= 0 || pt.X >= this.Width || pt.Y >= this.Height)
            {
                this.ShowFloatButtons(false);
            }
        }
        #endregion

        #region Cell Drag: imgCell_MouseLeave() imgCell_Drop()
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ImageCell imgCell = sender as ImageCell;
                if (imgCell != null && !string.IsNullOrEmpty(imgCell.ImageSource))
                {
                    DragDrop.DoDragDrop(imgCell, imgCell, DragDropEffects.Move);
                }
            }
        }

        private void UserControl_Drop(object sender, DragEventArgs e)
        {
            ImageCell droppedCell = sender as ImageCell;
            if (droppedCell != null)
            {
                string dataFormat = e.Data.GetFormats()[0];
                ImageCell draggedCell = e.Data.GetData(dataFormat) as ImageCell;
                if (draggedCell != null)
                {
                    string droppedImageSource = droppedCell.ImageSource;
                    Rotation droppedImageRotation = draggedCell.ImageRotation;
                    droppedCell.OpenImage(draggedCell.ImageSource, draggedCell.ImageRotation);
                    draggedCell.OpenImage(droppedImageSource, droppedImageRotation);
                }
                else
                {
                    ImageThumb draggedThumb = e.Data.GetData(dataFormat) as ImageThumb;
                    if (draggedThumb != null)
                    {
                        droppedCell.OpenImage(draggedThumb.ImageSource, Rotation.Rotate0);
                    }
                }
            }
        }
        #endregion

        #region ShowFloatButtons()
        private void ShowFloatButtons(bool show)
        {
            if (show)
            {
                this.btnClose.Visibility = Visibility.Visible;
                this.btnTurnLeft.Visibility = Visibility.Visible;
                this.btnTurnRight.Visibility = Visibility.Visible;
            }
            else
            {
                this.btnClose.Visibility = Visibility.Hidden;
                this.btnTurnLeft.Visibility = Visibility.Hidden;
                this.btnTurnRight.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }//end of class
}



#region //CroppedBitmap
//CroppedBitmap cb = new CroppedBitmap(image, new Int32Rect((int)((image.PixelWidth / 2) - (targetWidth / 2)), 0, (int)targetWidth, (int)targetHeight));


/*
BitmapImage _reducedImage = null;
MemoryStream mem;
// Only load thumbnails
byte[] buffer = File.ReadAllBytes(UriSource.AbsolutePath);
mem = new MemoryStream(buffer);
_reducedImage = new BitmapImage();
_reducedImage.BeginInit();
_reducedImage.CacheOption = BitmapCacheOption.None;
_reducedImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
_reducedImage.DecodePixelWidth = DecodeWidth;
_reducedImage.DecodePixelHeight = DecodeHeight;
_reducedImage.StreamSource = mem;
_reducedImage.Rotation = Rotation.Rotate0;
_reducedImage.EndInit();
buffer = null;
return _reducedImage;
*/

#endregion