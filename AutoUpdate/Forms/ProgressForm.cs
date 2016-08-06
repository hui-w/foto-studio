using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;

namespace QLike.AutoUpdate
{
    public partial class ProgressForm : Form
    {
        private bool isFinished = false;
        private string updateUrl = string.Empty;
        private List<AppFileInfo> downloadList = null;
        private ManualResetEvent evtDownload = null;
        private ManualResetEvent evtPerDonwload = null;
        private WebClient clientDownload = null;
        long totalBytes = 0;
        long downloadedBytes = 0;

        public ProgressForm(string updateUrl, List<AppFileInfo> downloadList)
        {
            this.downloadList = downloadList;
            this.updateUrl = updateUrl;
            InitializeComponent();
        }

        private void DownloadProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isFinished && DialogResult.No == MessageBox.Show("Are you sure to cancel the updating?", "Auto Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                e.Cancel = true;
                return;
            }
            else
            {
                if (clientDownload != null)
                {
                    clientDownload.CancelAsync();
                }

                evtDownload.Set();
                evtPerDonwload.Set();
            }
        }

        private void DownloadProgress_Load(object sender, EventArgs e)
        {
            this.evtDownload = new ManualResetEvent(true);
            this.evtDownload.Reset();
            Thread t = new Thread(new ThreadStart(ProcDownload));
            t.Name = "download";
            t.Start();
        }

        private void ProcDownload()
        {
            this.evtPerDonwload = new ManualResetEvent(false);

            foreach (AppFileInfo file in this.downloadList)
            {
                totalBytes += file.Size;
            }

            while (!this.evtDownload.WaitOne(0, false))
            {
                if (this.downloadList.Count == 0)
                    break;

                AppFileInfo file = this.downloadList[0];

                this.ShowCurrentDownloadFileName(file.Path);

                //Download
                this.clientDownload = new WebClient();
                this.clientDownload.Proxy = WebRequest.DefaultWebProxy;
                this.clientDownload.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

                this.clientDownload.DownloadProgressChanged += new DownloadProgressChangedEventHandler(OnDownloadProgressChanged);
                this.clientDownload.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFileCompleted);

                this.evtPerDonwload.Reset();

                string fileServerUrl = Common.GetServerSideUrl(this.updateUrl, file.Path);
                string fileClientPath = string.Concat(Common.ClientFolder, file.Path + ".tmp");
                Common.PrepareClientFolder(fileClientPath);
                this.clientDownload.DownloadFileAsync(new Uri(fileServerUrl), fileClientPath, file);

                //waiting for download complete
                this.evtPerDonwload.WaitOne();

                this.clientDownload.Dispose();
                this.clientDownload = null;

                //remove downloaded file
                this.downloadList.Remove(file);
            }

            if (this.downloadList.Count == 0)
            {
                Exit(true);
            }
            else
            {
                Exit(false);
            }

            evtDownload.Set();
        }

        void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            AppFileInfo file = e.UserState as AppFileInfo;
            this.downloadedBytes += file.Size;
            this.SetProcessBar(this.downloadedBytes, this.totalBytes);

            string filePath = Common.CombinePath(Common.ClientFolder, file.Path, false);
            if (File.Exists(filePath))
            {
                if (File.Exists(filePath + ".old"))
                    File.Delete(filePath + ".old");

                File.Move(filePath, filePath + ".old");
            }

            File.Move(filePath + ".tmp", filePath);
            
            //coutinue to download other file
            evtPerDonwload.Set();
        }

        void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //this.SetProcessBar(e.ProgressPercentage, (int)((nDownloadedTotal + e.BytesReceived) * 100 / totalBytes));
            this.SetProcessBar(this.downloadedBytes + e.BytesReceived, totalBytes);
        }

        delegate void ShowCurrentDownloadFileNameCallBack(string name);
        private void ShowCurrentDownloadFileName(string name)
        {
            if (this.labelCurrentItem.InvokeRequired)
            {
                ShowCurrentDownloadFileNameCallBack cb = new ShowCurrentDownloadFileNameCallBack(ShowCurrentDownloadFileName);
                this.Invoke(cb, new object[] { name });
            }
            else
            {
                this.labelCurrentItem.Text = string.Format("Downloading: {0} ...", name);
            }
        }

        delegate void SetProcessBarCallBack(long current, long total);
        private void SetProcessBar(long current, long total)
        {
            if (this.progressBarTotal.InvokeRequired)
            {
                SetProcessBarCallBack cb = new SetProcessBarCallBack(SetProcessBar);
                this.Invoke(cb, new object[] { current, total });
            }
            else
            {
                this.lblDownloadPercentage.Text = string.Format("Downloaded {0} of {1} ({2}%)", 
                    Common.FormatFileSize(current), 
                    Common.FormatFileSize(total),
                    current * 100 / total
                    );
                this.progressBarTotal.Value = (int)(current * 100 / total);
            }
        }

        delegate void ExitCallBack(bool success);
        private void Exit(bool success)
        {
            if (this.InvokeRequired)
            {
                ExitCallBack cb = new ExitCallBack(Exit);
                this.Invoke(cb, new object[] { success });
            }
            else
            {
                this.isFinished = success;
                this.DialogResult = success ? DialogResult.OK : DialogResult.Cancel;
                this.Close();
            }
        }
    }//end of class
}
