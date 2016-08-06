using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLike.AutoUpdate;
using System.IO;

namespace QLike.Foto.Launcher
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            int appCount = 0;
            int margin = 5;
            int top = 5;

            string currentDir = AppDomain.CurrentDomain.BaseDirectory;

            string fotoGridExecutable = string.Concat(currentDir, "FotoGridStudio.exe");
            if (File.Exists(fotoGridExecutable))
            {
                pnlApp fotoGrid = new pnlApp("Foto Grid Studio", "app_grid_48", fotoGridExecutable);
                fotoGrid.Left = margin;
                fotoGrid.Top = top;
                top += fotoGrid.Height + margin;
                this.pnlMain.Controls.Add(fotoGrid);
                appCount++;
            }

            string exifViewerExecutable = string.Concat(currentDir, "ExifViewer.exe");
            if (File.Exists(exifViewerExecutable))
            {
                pnlApp exifViewer = new pnlApp("Exif Viewer", "app_exif_48", exifViewerExecutable);
                exifViewer.Left = margin;
                exifViewer.Top += top;
                top += exifViewer.Height + margin;
                this.pnlMain.Controls.Add(exifViewer);
                appCount++;
            }

            if (appCount == 0)
            {
                //show not loaded
                this.menuSeparatorExit.Visible = false;
                this.menuUpdate.Visible = false;
                pnlDownload pnl = new pnlDownload();
                this.pnlMain.Controls.Add(pnl);
            }
            else
            {
                //show update
                pnlApp updater = new pnlApp("Check Updates", "app_updater_48", string.Empty);
                updater.Left = 2 * margin + updater.Width;
                updater.Top = margin;
                top += updater.Height + margin;
                this.pnlMain.Controls.Add(updater);
                updater.onClick += new pnlApp.ClickHandler(updater_onClick);
            }
        }

        void updater_onClick(object sender, string e)
        {
            this.CheckUpdate();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuUpdate_Click(object sender, EventArgs e)
        {
            this.CheckUpdate();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void CheckUpdate()
        {
            Updater up = new Updater(false, true);
            up.Update();
        }
    }//end of class
}
