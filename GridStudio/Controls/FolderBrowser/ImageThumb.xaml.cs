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

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for Thumb.xaml
    /// </summary>
    public partial class ImageThumb : UserControl, IThumb
    {
        public string ImageSource
        {
            get;
            set;
        }

        public ImageThumb(int width, int height, string imageSource)
        {
            InitializeComponent();

            this.Width = width;
            this.Height = height;
            this.ImageSource = imageSource;

            this.OpenImage();
        }

        public void OpenImage()
        {
            if(string.IsNullOrEmpty(this.ImageSource))
            {
                return;
            }

            try
            {
                //Show thumb
                Size resizedToSize;
                this.image.Source = Utility.GetFilledThumb(
                    this.ImageSource,
                    new Size(this.Width, this.Height),
                    Rotation.Rotate0,
                    true,
                    out resizedToSize);
            }
            catch
            {
                //MessageBox.Show(ex.Message);
                //Environment.Exit(0);
            }
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageThumb imgThumb = sender as ImageThumb;
            if (imgThumb != null)
            {
                DragDrop.DoDragDrop(imgThumb, imgThumb, DragDropEffects.Copy);
            }
        }
    }//end of class
}
