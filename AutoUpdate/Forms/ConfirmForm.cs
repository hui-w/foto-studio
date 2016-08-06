using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLike.AutoUpdate
{
    public partial class ConfirmForm : Form
    {
        private List<AppFileInfo> downloadList = null;
        private bool isUpdating = true;

        public ConfirmForm(List<AppFileInfo> downloadList, bool isUpdating)
        {
            this.downloadList = downloadList;
            this.isUpdating = isUpdating;
            InitializeComponent();
        }

        private void DownloadConfirm_Load(object sender, EventArgs e)
        {
            if (this.isUpdating)
            {
                this.Text = "Updates Available";
                this.btnOk.Text = "&Update";
                this.btnCancel.Text = "&Ignore";
            }
            else
            {
                this.Text = "Files to Download";
                this.btnOk.Text = "&Download";
                this.btnCancel.Text = "&Cancel";
            }

            foreach (AppFileInfo file in this.downloadList)
            {
                ListViewItem item = new ListViewItem(new string[] { file.Path, Common.FormatFileSize(file.Size) });
                this.listDownloadFile.Items.Add(item);
            }

            this.Activate();
            this.Focus();
        }

        private void listDownloadFile_Resize(object sender, EventArgs e)
        {
            //this.listDownloadFile.Columns[0].Width = this.listDownloadFile.Width - 120;
            //this.listDownloadFile.Columns[1].Width = 200;
        }
    }
}
