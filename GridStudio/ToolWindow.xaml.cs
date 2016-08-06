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
using System.Deployment.Application;
using System.IO;

namespace QLike.Foto.GridStudio
{
    /// <summary>
    /// Interaction logic for ToolWindow.xaml
    /// </summary>
    public partial class ToolWindow : Window
    {
        public ToolWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
#if DEBUG
            sb.AppendLine("#if DEBUG == TRUE");
            sb.AppendLine();
#endif
            sb.AppendLine(string.Format("ApplicationDeployment.IsNetworkDeployed = {0}", ApplicationDeployment.IsNetworkDeployed));
            sb.AppendLine();
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                sb.AppendLine("ApplicationDeployment.CurrentDeployment.DataDirectory:");
                sb.AppendLine(ApplicationDeployment.CurrentDeployment.DataDirectory);
                sb.AppendLine();
            }
            sb.AppendLine("Directory.GetCurrentDirectory():");
            sb.AppendLine(Directory.GetCurrentDirectory());
            sb.AppendLine();
            this.txtOutput.Text = sb.ToString();

            //background
            //EllipseGeometry ellipse = new EllipseGeometry(new Point(50, 50), 50, 20);
            //RectangleGeometry rect = new RectangleGeometry(new Rect(50, 50, 50, 20), 5, 5);
            //PathGeometry combin = Geometry.Combine(ellipse, rect, GeometryCombineMode.Xor, null);
            //GeometryDrawing drawing = new GeometryDrawing(Brushes.LightBlue, new Pen(Brushes.Green, 2), combin);
            //DrawingBrush background = new DrawingBrush(drawing);
            //background.Viewport = new Rect(0, 0, 0.15, 0.15);
            //background.TileMode = TileMode.Tile;
            //this.Background = background;
        }

        private void btnWinformDlg_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Title = "Select Image";
            dlg.Filter = "JPG File(.jpg）|*.jpg|All Files|*.*";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }

        private void btnMicrosoftDlg_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "Select Image";
            dlg.Filter = "JPG File(.jpg）|*.jpg|All Files|*.*";
            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {

            }
        }

        private void btnFolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.Description = "Please choose the root of repository...";
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
    }//end of class
}
