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
    /// Interaction logic for TurnRightButton.xaml
    /// </summary>
    public partial class TurnRightButton : UserControl
    {
        public TurnRightButton()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            double gridWidth = this.grid.ActualWidth;
            double gridHeight = this.grid.ActualHeight;
            double thickness = gridWidth / 10;
            Brush borderBrush = Brushes.White;

            Rectangle rect = new Rectangle();
            rect.StrokeThickness = thickness / 2;
            rect.Stroke = borderBrush;
            rect.Fill = Brushes.LightSteelBlue;
            rect.Margin = new Thickness(0, 0, 0, 0);
            this.grid.Children.Add(rect);

            Polyline line = new Polyline();
            line.Points.Add(new Point(thickness * 2, thickness * 2));
            line.Points.Add(new Point(gridWidth - thickness * 4, thickness * 2));
            line.Points.Add(new Point(gridWidth - thickness * 4, gridHeight - thickness * 2));
            line.StrokeThickness = thickness;
            line.Stroke = borderBrush;
            this.grid.Children.Add(line);

            Polygon gon = new Polygon();
            gon.Points.Add(new Point(gridWidth - thickness * 2, gridHeight - thickness * 4));
            gon.Points.Add(new Point(gridWidth - thickness * 4, gridHeight - thickness * 2));
            gon.Points.Add(new Point(gridWidth - thickness * 6, gridHeight - thickness * 4));
            gon.Fill = borderBrush;
            this.grid.Children.Add(gon);
        }
    }
}
