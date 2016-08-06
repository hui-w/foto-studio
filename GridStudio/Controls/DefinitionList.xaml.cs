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
using System.Deployment.Application;
using System.IO;

namespace QLike.Foto.GridStudio.Controls
{
    /// <summary>
    /// Interaction logic for DefinitionList.xaml
    /// </summary>
    public partial class DefinitionList : UserControl
    {
        public delegate void SelectionChangedHandler(object sender, SelectionChangedEventArgs e);
        public event SelectionChangedHandler onSelectionChanged;

        public DefinitionList()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = double.NaN;
            this.Height = double.NaN;
            this.LoadDifinitionFiles();
        }

        private void LoadDifinitionFiles()
        {
            try
            {
#if DEBUG
                Directory.SetCurrentDirectory("../../");
#endif
                string path = string.Empty;
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    path = string.Concat(ApplicationDeployment.CurrentDeployment.DataDirectory, "\\GridDefinition");
                }
                else
                {
                    path = string.Concat(Directory.GetCurrentDirectory(), "\\GridDefinition");
                }
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach (FileInfo file in dir.GetFiles("*.xml"))
                {
                    this.lstDifinition.Items.Add(file);
                }
                this.lstDifinition.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void lstDifinition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (onSelectionChanged != null)
            {
                onSelectionChanged(sender, e);
            }
        }
    }//end of class
}
