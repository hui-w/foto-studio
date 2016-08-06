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

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for TipBoard.xaml
    /// </summary>
    public partial class TipBoard : UserControl
    {
        private string tipContent = string.Empty;

        public string TipContent
        {
            set
            {
                tipContent = value;
                this.lblContent.Content = value;
            }
        }

        public TipBoard(string tipContent)
        {
            InitializeComponent();
            this.tipContent = tipContent;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblContent.Content = this.tipContent;
        }
    }
}
