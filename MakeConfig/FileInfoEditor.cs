using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLike.AutoUpdate
{
    public partial class FileInfoEditor : UserControl
    {
        public bool Checked
        {
            get
            {
                return this.chkEnabled.Checked;
            }
            set
            {
                this.chkEnabled.Checked = value;
                this.txtVersion.Enabled = value;
            }
        }

        public bool NeedRestart
        {
            get
            {
                return this.chkRestart.Checked;
            }
            set
            {
                this.chkRestart.Checked = value;
            }
        }

        public string Version
        {
            get
            {
                return this.txtVersion.Text.Trim();
            }
            set
            {
                this.txtVersion.Text = value;
            }
        }

        public string Path
        {
            get
            {
                return this.lblPath.Text.Trim();
            }
            set
            {
                this.lblPath.Text = value;
            }
        }

        public long FileLength
        {
            get;
            set;
        }

        private int index = 0;

        public FileInfoEditor(int index)
        {
            this.index = index;
            InitializeComponent();
        }

        private void FileInfoEditor_Resize(object sender, EventArgs e)
        {
            this.lblPath.Width = this.Width - 210;
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.Checked = this.chkEnabled.Checked;
        }

        private void FileInfoEditor_Load(object sender, EventArgs e)
        {
            this.lblIndex.Text = this.index.ToString();
        }
    }
}
