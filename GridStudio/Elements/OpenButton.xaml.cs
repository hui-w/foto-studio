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
    /// Interaction logic for AddButton.xaml
    /// </summary>
    public partial class OpenButton : UserControl
    {
        public OpenButton()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = this.grid.ActualWidth;
            double gridHeight = this.grid.ActualHeight;
            double thickness = gridWidth / 40;
            Brush borderBrush = Brushes.SlateGray;
            DoubleCollection dashArray = new DoubleCollection(new List<double>() {4, 1});

            Ellipse ellipse = new Ellipse();
            ellipse.StrokeThickness = thickness;
            ellipse.Stroke = borderBrush;
            ellipse.StrokeDashArray = dashArray;
            ellipse.Fill = Brushes.LightGray;
            ellipse.Margin = new Thickness(0, 0, 0, 0);
            this.grid.Children.Add(ellipse);

            Rectangle rect1 = new Rectangle();
            rect1.Fill = borderBrush;
            rect1.Width = thickness;
            rect1.Height = gridHeight / 2;
            this.grid.Children.Add(rect1);

            Rectangle rect2 = new Rectangle();
            rect2.Fill = borderBrush;
            rect2.Width = gridWidth / 2;
            rect2.Height = thickness;
            this.grid.Children.Add(rect2);
        }
    }
}
