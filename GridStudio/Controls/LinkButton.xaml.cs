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
    /// Interaction logic for LinkButton.xaml
    /// </summary>
    public partial class LinkButton : UserControl
    {
        enum ButtonState
        {
            Normal,
            Checked,
            Hover
        }

        public string Text
        {
            get;
            set;
        }

        private bool isChecked;

        public bool IsChecked
        {
            get
            {
                return this.isChecked;
            }
            set
            {
                this.isChecked = value;
                if (this.isChecked)
                {
                    this.SetButtonStyle(ButtonState.Checked);
                }
                else
                {
                    this.SetButtonStyle(ButtonState.Normal);
                }
            }
        }

        public LinkButton()
        {
            InitializeComponent();
        }

        private void SetButtonStyle(ButtonState state)
        {
            switch (state)
            {
                case ButtonState.Checked:
                    this.border.Background = Brushes.Gray;
                    this.border.BorderBrush = Brushes.Gray;
                    this.lblContent.Foreground = Brushes.White;
                    break;
                case ButtonState.Hover:
                    this.border.Background = new SolidColorBrush(Color.FromRgb(0xC2, 0xE0, 0xFF));
                    this.border.BorderBrush = new SolidColorBrush(Color.FromRgb(0x33, 0x99, 0xFF));
                    this.lblContent.Foreground = Brushes.Black;
                    break;
                case ButtonState.Normal:
                    this.border.Background = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF));
                    this.border.BorderBrush = new SolidColorBrush(Color.FromRgb(0x66, 0x66, 0x66));
                    this.lblContent.Foreground = Brushes.Black;
                    break;
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblContent.Content = this.Text;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            this.SetButtonStyle(ButtonState.Hover);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.isChecked)
            {
                this.SetButtonStyle(ButtonState.Checked);
            }
            else
            {
                this.SetButtonStyle(ButtonState.Normal);
            }
        }
    }//end of class
}
