using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace QLike.Foto.BatchResizer
{
    public partial class frmMain : Form
    {
        private int initialWindowWidth;
        private int initialTextWidth;

        private ThumbType selectedType = ThumbType.InsideUniform;

        public frmMain()
        {
            InitializeComponent();
        }

        #region frmMain_Load()
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.initialWindowWidth = this.Width;
            this.initialTextWidth = this.txtSource.Width;

            this.pnlProperties.MinimumSize = this.pnlProperties.Size;

            //Show types
            this.radioType1.Text = ThumbType.InsideUniform.ToString();
            this.radioType1.Tag = ThumbType.InsideUniform;
            this.radioType2.Text = ThumbType.OutsideUniform.ToString();
            this.radioType2.Tag = ThumbType.OutsideUniform;
            this.radioType3.Text = ThumbType.CorpToFill.ToString();
            this.radioType3.Tag = ThumbType.CorpToFill;
            this.radioType1.Checked = true;
        }
        #endregion

        #region frmMain_Resize()
        private void frmMain_Resize(object sender, EventArgs e)
        {
            int width = this.Width > 300 ? this.Width : 300;
            this.txtSource.Width = this.initialTextWidth + width - this.initialWindowWidth;
            this.linkSource.Left = this.txtSource.Left + this.txtSource.Width + 6;

            this.txtTarget.Width = this.initialTextWidth + width - this.initialWindowWidth;
            this.linkTarget.Left = this.txtTarget.Left + this.txtTarget.Width + 6;
        }
        #endregion

        #region BrowseFolder()
        private bool BrowseFolder(TextBox txt)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = txt.Text;
            dlg.Description = "Please choose the directory...";
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txt.Text = dlg.SelectedPath;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region linkSource_LinkClicked(), txtSource_Leave(), linkTarget_LinkClicked(), txtTarget_TextChanged
        private void linkSource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!this.BrowseFolder(this.txtSource))
            {
                return;
            }

            //List files
            DirectoryInfo dir = new DirectoryInfo(this.txtSource.Text);
            int count = 0;
            foreach (FileInfo file in dir.GetFiles("*.jpg"))
            {
                this.lstPreview.Items.Add(file);
                count++;
            }
            this.lblStatus.Text = string.Format("{0} JPG files found in the source folder", count);
        }

        private void txtSource_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTarget.Text) && !string.IsNullOrEmpty(this.txtSource.Text))
            {
                this.txtTarget.Text = string.Concat(this.txtSource.Text, "\\thumb");
            }
        }

        private void linkTarget_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.BrowseFolder(this.txtTarget);
        }

        private void txtTarget_TextChanged(object sender, EventArgs e)
        {
            this.btnOpenTarget.Visible = !string.IsNullOrEmpty(this.txtTarget.Text);
        }
        #endregion

        #region menuExit_Click(), menuAbout_Click()
        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A tool for batch creating thumbnails\n\nby QLike.com", "About Batch Resizer...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        #endregion

        #region radioType_CheckedChanged()
        private void radioType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdo = sender as RadioButton;
            if (rdo.Checked)
            {
                this.selectedType = (ThumbType)rdo.Tag;
                if (this.selectedType == ThumbType.CorpToFill)
                {
                    this.pictureType.Image = Properties.Resources.corp;
                }
                else if (this.selectedType == ThumbType.InsideUniform)
                {
                    this.pictureType.Image = Properties.Resources.inner;
                }
                else if (this.selectedType == ThumbType.OutsideUniform)
                {
                    this.pictureType.Image = Properties.Resources.outter;
                }
            }
        }
        #endregion

        #region btnStart_Click()
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //Get input
                ThumbParam param = new ThumbParam();
                param.sourceFolder = this.txtSource.Text.Trim();
                param.targetFolder = this.txtTarget.Text.Trim();
                param.targetWidth = (int)this.numThumbWidth.Value;
                param.targetHeight = (int)this.numThumbHeight.Value;
                param.jpegQuality = (int)this.numQuality.Value;
                param.overwrite = this.chkOverwrite.Checked;

                if (!param.targetFolder.EndsWith("\\"))
                {
                    param.targetFolder += "\\";
                }

                if (string.IsNullOrEmpty(param.sourceFolder) || string.IsNullOrEmpty(param.targetFolder))
                {
                    MessageBox.Show("Source or target cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!Directory.Exists(param.targetFolder))
                {
                    DialogResult result = MessageBox.Show(string.Format("\"{0}\" doesn't exist. \nDo you want to create it?", param.targetFolder), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        //Prepare folder
                        Common.PrepareFolder(param.targetFolder);
                    }
                    else
                    {
                        this.lblStatus.Text = "Aborted";
                        return;
                    }
                }

                //Clear the list for output log
                this.lstPreview.Items.Clear();
                this.EnableForm(false);

                //Process files
                Thread t = new Thread(new ParameterizedThreadStart(Proc));
                t.Name = "process";
                t.Start(param);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }
        #endregion

        #region Proc()
        private void Proc(object objParam)
        {
            try
            {
                ThumbParam param = (ThumbParam)objParam;
                DirectoryInfo dir = new DirectoryInfo(param.sourceFolder);
                FileInfo[] files = dir.GetFiles("*.jpg");
                int countAll = files.Length;
                int countResized = 0;
                for (int i = 0; i < countAll; i++)
                {
                    int progressValue = i * 100 / countAll;
                    this.SetProgress(progressValue);

                    FileInfo file = files[i];
                    this.ShowStatus(string.Format("Processing {0} of {1} files. {2}% Finished", i + 1, files.Length, progressValue));
                    Size newSize;
                    int ret = Common.CreateThumb(file,
                        this.selectedType,
                        param.targetFolder,
                        param.targetWidth,
                        param.targetHeight,
                        param.jpegQuality,
                        param.overwrite,
                        out newSize);
                    if (ret == 0)
                    {
                        this.AppendToList(string.Format("Skipped: {0} - is smalled than the targets", file.Name));
                    }
                    else if (ret == 1)
                    {
                        this.AppendToList(string.Format("{0} resized to {1}X{2}", file.Name, param.targetWidth, param.targetHeight));
                        countResized++;
                    }
                    else if (ret == 2)
                    {
                        this.AppendToList(string.Format("Skipped: {0} - already existed in the target folder", file.Name));
                    }
                }
                this.ShowStatus(string.Format("{0} JPG files resized to the target folder", countResized));
                this.EnableForm(true);
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        delegate void SetProgressCallBack(int value);
        private void SetProgress(int value)
        {
            if (this.InvokeRequired)
            {
                SetProgressCallBack cb = new SetProgressCallBack(SetProgress);
                this.Invoke(cb, new object[] { value });
            }
            else
            {
                this.progressBar.Value = value;
            }
        }

        delegate void AppendToListCallBack(string content);
        private void AppendToList(string content)
        {
            if (this.lstPreview.InvokeRequired)
            {
                AppendToListCallBack cb = new AppendToListCallBack(AppendToList);
                this.Invoke(cb, new object[] { content });
            }
            else
            {
                this.lstPreview.Items.Add(content);
            }
        }

        delegate void ShowStatusCallBack(string content);
        private void ShowStatus(string content)
        {
            if (this.InvokeRequired)
            {
                ShowStatusCallBack cb = new ShowStatusCallBack(ShowStatus);
                this.Invoke(cb, new object[] { content });
            }
            else
            {
                this.lblStatus.Text = content;
            }
        }

        delegate void EnableFormCallBack(bool enabled);
        private void EnableForm(bool enabled)
        {
            if (this.InvokeRequired)
            {
                EnableFormCallBack cb = new EnableFormCallBack(EnableForm);
                this.Invoke(cb, new object[] { enabled });
            }
            else
            {
                this.pnlSrcDest.Enabled = enabled;
                this.pnlProperties.Enabled = enabled;
                this.btnStart.Enabled = enabled;
                this.progressBar.Visible = !enabled;
            }
        }
        #endregion

        struct ThumbParam
        {
            public string sourceFolder;
            public string targetFolder;
            public int targetWidth;
            public int targetHeight;
            public int jpegQuality;
            public bool overwrite;
        }

        private void btnOpenTarget_Click(object sender, EventArgs e)
        {
            string path = this.txtTarget.Text.Trim();
            if (Directory.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                this.ShowError("Target not exists!");
            }
        }

        private void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }//end of class
}
