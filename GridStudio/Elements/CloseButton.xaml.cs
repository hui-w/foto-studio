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

namespace QLike.Foto.GridStudio.Elements
{
    /// <summary>
    /// Interaction logic for DelButton.xaml
    /// </summary>
    public partial class CloseButton : UserControl
    {
        public CloseButton()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = this.grid.ActualWidth;
            double thickness = gridWidth / 10;
            Brush borderBrush = Brushes.White;
            //DoubleCollection dashArray = new DoubleCollection(new List<double>() { 4, 0 });

            Rectangle rect = new Rectangle();
            rect.StrokeThickness = thickness / 2;
            rect.Stroke = borderBrush;
            //rect.StrokeDashArray = dashArray;
            rect.Fill = Brushes.Crimson;
            rect.Margin = new Thickness(0, 0, 0, 0);
            this.grid.Children.Add(rect);

            Line line1 = new Line();
            line1.Stroke = borderBrush;
            line1.StrokeThickness = thickness;
            line1.X1 = this.ActualWidth / 4;
            line1.Y1 = this.ActualHeight / 4;
            line1.X2 = gridWidth - this.ActualWidth / 4;
            line1.Y2 = gridWidth - this.ActualHeight / 4;
            this.grid.Children.Add(line1);

            Line line2 = new Line();
            line2.Stroke = borderBrush;
            line2.StrokeThickness = thickness;
            line2.X1 = gridWidth - this.ActualWidth / 4;
            line2.Y1 = this.ActualHeight / 4;
            line2.X2 = this.ActualWidth / 4;
            line2.Y2 = gridWidth - this.ActualHeight / 4;
            this.grid.Children.Add(line2);
        }
    }
}
