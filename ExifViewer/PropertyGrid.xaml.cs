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

namespace QLike.Foto.ExifViewer
{
    /// <summary>
    /// Interaction logic for PropertyGrid.xaml
    /// </summary>
    public partial class PropertyGrid : UserControl
    {
        private Dictionary<string, string> exifDict = new Dictionary<string, string>();

        public Dictionary<string, string> ExifDict
        {
            get { return exifDict; }
            set { exifDict = value; }
        }

        public string HeaderText
        {
            get;
            set;
        }

        public PropertyGrid()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.RenderGrid();
        }

        public void RenderGrid()
        {
            this.txtHeader.Text = this.HeaderText;

            this.gridKeyValues.ColumnDefinitions.Add(new ColumnDefinition());
            this.gridKeyValues.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < this.exifDict.Count; i++)
            {
                this.gridKeyValues.RowDefinitions.Add(new RowDefinition());
            }

            //Grid.SetRowSpan(this.gridSplitter, this.dict.Count);

            int rowIndex = 0;
            foreach (string key in this.exifDict.Keys)
            {
                if (this.exifDict[key] == "NULL")
                {
                    //Key
                    TextBlock txtSeparator = new TextBlock();
                    Grid.SetColumn(txtSeparator, 0);
                    Grid.SetRow(txtSeparator, rowIndex);
                    Grid.SetColumnSpan(txtSeparator, 2);
                    txtSeparator.Margin = new Thickness(5, 2, 2, 2);
                    txtSeparator.Text = key;
                    txtSeparator.ToolTip = key;
                    txtSeparator.FontWeight = FontWeights.Bold;
                    this.gridKeyValues.Children.Add(txtSeparator);

                    Border borderSeparator = new Border();
                    Grid.SetColumn(borderSeparator, 0);
                    Grid.SetRow(borderSeparator, rowIndex);
                    Grid.SetColumnSpan(borderSeparator, 2);
                    this.gridKeyValues.Children.Add(borderSeparator);

                    if (rowIndex == 0)
                    {
                        borderSeparator.BorderThickness = new Thickness(0, 1, 0, 1);
                    }
                    else
                    {
                        borderSeparator.BorderThickness = new Thickness(0, 0, 0, 1);
                    }
                }
                else
                {
                    //Key
                    TextBlock txtKey = new TextBlock();
                    Grid.SetColumn(txtKey, 0);
                    Grid.SetRow(txtKey, rowIndex);
                    txtKey.Margin = new Thickness(5, 2, 2, 2);
                    txtKey.Text = key;
                    txtKey.ToolTip = key;
                    this.gridKeyValues.Children.Add(txtKey);

                    Border borderKey = new Border();
                    Grid.SetColumn(borderKey, 0);
                    Grid.SetRow(borderKey, rowIndex);
                    this.gridKeyValues.Children.Add(borderKey);

                    if (rowIndex == 0)
                    {
                        borderKey.BorderThickness = new Thickness(0, 1, 1, 1);
                    }
                    else
                    {
                        borderKey.BorderThickness = new Thickness(0, 0, 1, 1);
                    }

                    //Value
                    TextBox txtValue = new TextBox();
                    Grid.SetColumn(txtValue, 1);
                    Grid.SetRow(txtValue, rowIndex);
                    txtValue.Margin = new Thickness(5, 2, 0, 2);
                    txtValue.BorderThickness = new Thickness(0);
                    txtValue.Text = this.exifDict[key];
                    txtValue.ToolTip = this.exifDict[key];
                    this.gridKeyValues.Children.Add(txtValue);

                    Border borderValue = new Border();
                    Grid.SetColumn(borderValue, 1);
                    Grid.SetRow(borderValue, rowIndex);
                    this.gridKeyValues.Children.Add(borderValue);

                    if (rowIndex == 0)
                    {
                        borderValue.BorderThickness = new Thickness(0, 1, 0, 1);
                    }
                    else
                    {
                        borderValue.BorderThickness = new Thickness(0, 0, 0, 1);
                    }
                }

                rowIndex++;
            }
        }
    }//end of class
}
