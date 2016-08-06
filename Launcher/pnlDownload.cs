using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLike.AutoUpdate;

namespace QLike.Foto.Launcher
{
    public partial class pnlDownload : UserControl
    {
        private Updater updater;

        public pnlDownload()
        {
            this.updater = new Updater(false, false);
            //this.updater.IsUpdating = false;
            //this.updater.IsBackend = false;

            InitializeComponent();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            this.updater.Update();
        }
    }
}
