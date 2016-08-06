using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace QLike.AutoUpdate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.SelectedPath = this.txtFolder.Text;
            dlg.Description = "Please choose the directory...";
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string root = dlg.SelectedPath;
                this.txtFolder.Text = root;

                DirectoryInfo dir = new DirectoryInfo(root);
                IEnumerable<FileInfo> files = from file in dir.GetFiles("*", SearchOption.AllDirectories)
                                   where (file.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden
                                   select file;
                int index = files.Count(); ;
                foreach(FileInfo file in files)
                {
                    FileInfoEditor editor = new FileInfoEditor(index --);
                    editor.Dock = DockStyle.Top;
                    editor.Path = file.FullName.Substring(root.Length);
                    editor.FileLength = file.Length;
                    this.pnlMain.Controls.Add(editor);
                }

                this.btnSave.Enabled = true;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool checkedAll = this.chkAll.Checked;
            foreach (FileInfoEditor editor in this.pnlMain.Controls)
            {
                editor.Checked = checkedAll;
            }
        }

        private void btnApplyAll_Click(object sender, EventArgs e)
        {
            string version = this.txtVersion.Text.Trim();
            foreach (FileInfoEditor editor in this.pnlMain.Controls)
            {
                editor.Version = version;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string file = string.Concat(this.txtFolder.Text, "\\Version.xml");
                ConfigInfo config = new ConfigInfo();
                config.UpdateUrl = this.txtUrl.Text.Trim();
                config.Version = this.txtVersion.Text.Trim();
                foreach (FileInfoEditor editor in this.pnlMain.Controls)
                {
                    if (!editor.Checked)
                    {
                        continue;
                    }
                    AppFileInfo appFileInfo = new AppFileInfo();
                    appFileInfo.NeedRestart = editor.NeedRestart;
                    appFileInfo.Path = editor.Path;
                    appFileInfo.Size = editor.FileLength;
                    appFileInfo.Version = editor.Version;
                    config.FileList.Add(appFileInfo);
                }
                config.SaveConfigToFile(file);

                MessageBox.Show("Version.xml Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
