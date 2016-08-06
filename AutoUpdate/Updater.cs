using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace QLike.AutoUpdate
{
    public class Updater
    {
        public delegate void UpdateCompletedHandler(object sender, EventArgs e);
        public event UpdateCompletedHandler onUpdateCompleted;

        private ConfigInfo clientConfig = null;
        private bool needRestart = false;
        private List<AppFileInfo> downloadList;
        private List<AppFileInfo> deleteList;
        CheckingForm frmChecking;

        private bool isBackend = false;

        public bool IsBackend
        {
            get { return isBackend; }
            set { isBackend = value; }
        }
        private bool isUpdating = true;

        public bool IsUpdating
        {
            get { return isUpdating; }
            set { isUpdating = value; }
        }

        public Updater()
            : this(false, true)
        {
        }

        public Updater(bool backendCheck, bool isUpdating)
        {
            this.isBackend = backendCheck;
            this.isUpdating = isUpdating;
            try
            {
                //Load the configuration file
                this.clientConfig = ConfigInfo.LoadClientConfig(Common.ClientConfigFile);
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        public void Update()
        {
            try
            {
                if (this.clientConfig != null)
                {
                    //Start update
                    this.RetrieveServerConfig();
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        /// <summary>
        /// Show the prepare window and retrieve the server side config file
        /// </summary>
        private void RetrieveServerConfig()
        {
            try
            {
                //Get the server config
                string serverConfigFile = Common.GetServerSideUrl(this.clientConfig.UpdateUrl, Common.ServerConfigFileName);
                if (!this.isBackend)
                {
                    this.frmChecking = new CheckingForm(this.isUpdating);
                    frmChecking.Show();
                }
                Thread t = new Thread(new ThreadStart(delegate
                {
                    try
                    {
                        System.Threading.Thread.Sleep(2000);
                        WebClient client = new WebClient();
                        client.Proxy = WebRequest.DefaultWebProxy;
                        client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
                        client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ServerConfig_DownloadCompleted);
                        client.DownloadStringAsync(new Uri(serverConfigFile));
                    }
                    catch (Exception ex)
                    {
                        this.HandleException(ex);
                    }
                }));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        /// <summary>
        /// Got the server side config file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ServerConfig_DownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                this.ClosePrepareWindow(false);
                this.HandleException("Failed to retrieve server side config file", e.Error);
                return;
            }

            try
            {
                ConfigInfo serverConfig = ConfigInfo.LoadConfigFromXml(e.Result);
                if (!serverConfig.Enabled)
                {
                    //client disabled the update
                    this.ShowMessage("Update disabled by the server");
                    return;
                }

                this.downloadList = new List<AppFileInfo>();
                this.deleteList = new List<AppFileInfo>();

                if (serverConfig.Version != this.clientConfig.Version)
                {
                    //Update all files
                    this.deleteList = this.clientConfig.FileList;
                    this.downloadList = serverConfig.FileList;
                    this.needRestart = true;
                }
                else
                {
                    Dictionary<string, AppFileInfo> serverFiles = new Dictionary<string, AppFileInfo>();
                    foreach (AppFileInfo serverFile in serverConfig.FileList)
                    {
                        serverFiles.Add(serverFile.Path, serverFile);
                    }

                    foreach (AppFileInfo clientFile in this.clientConfig.FileList)
                    {
                        if (serverFiles.ContainsKey(clientFile.Path))
                        {
                            //check file version
                            AppFileInfo serverFile = serverFiles[clientFile.Path];
                            if (serverFile.Version != clientFile.Version)
                            {
                                //download for update
                                this.downloadList.Add(serverFile);
                                clientFile.Version = serverFile.Version;

                                if (!this.needRestart && serverFile.NeedRestart)
                                {
                                    this.needRestart = true;
                                }
                            }

                            serverFiles.Remove(clientFile.Path);
                        }
                        else
                        {
                            //delete
                            this.deleteList.Add(clientFile);
                            this.clientConfig.FileList.Remove(clientFile);
                        }
                    }

                    foreach (AppFileInfo serverFile in serverFiles.Values)
                    {
                        //newly added files
                        this.downloadList.Add(serverFile);

                        if (!this.needRestart && serverFile.NeedRestart)
                        {
                            this.needRestart = true;
                        }
                    }
                }

                bool hasUpdates = this.downloadList.Count > 0;

                //close the prepare window and show confirm window
                this.ClosePrepareWindow(hasUpdates);
                if (hasUpdates)
                {
                    this.clientConfig.Version = serverConfig.Version;
                    this.ConfirmDownload();
                }
                else
                {
                    this.ShowMessage("No updates available");
                }
            }
            catch (Exception ex)
            {
                this.HandleException( ex);
            }
        }

        delegate void ClosePrepareWindowCallBack(bool hasUpdates);
        /// <summary>
        /// Close the download prepare window
        /// </summary>
        /// <param name="hasUpdates"></param>
        private void ClosePrepareWindow(bool hasUpdates)
        {
            if (this.frmChecking != null && this.frmChecking.InvokeRequired)
            {
                ClosePrepareWindowCallBack cb = new ClosePrepareWindowCallBack(ClosePrepareWindow);
                this.frmChecking.Invoke(cb, new object[] { hasUpdates });
            }
            else
            {
                if (!this.isBackend)
                {
                    if (this.frmChecking != null)
                    {
                        this.frmChecking.Close();
                    }  
                }
            }
        }

        /// <summary>
        /// Show download confirm window
        /// </summary>
        private void ConfirmDownload()
        {
            ConfirmForm frmConfirm = new ConfirmForm(this.downloadList, this.isUpdating);
            if (DialogResult.OK == frmConfirm.ShowDialog())
            {
                //Delete files
                foreach (AppFileInfo file in this.deleteList)
                {
                    string filePath = Common.CombinePath(Common.ClientFolder, file.Path, false);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }

                this.StartDownload(this.downloadList);
            }
        }

        /// <summary>
        /// Show download progress window and start to download
        /// </summary>
        /// <param name="downloadList"></param>
        private void StartDownload(List<AppFileInfo> downloadList)
        {
            this.clientConfig.FileList.AddRange(downloadList);  //add download file list into the client config file
            ProgressForm frmProgress = new ProgressForm(this.clientConfig.UpdateUrl, downloadList);
            if (frmProgress.ShowDialog() == DialogResult.OK)
            {
                this.clientConfig.SaveConfigToFile(Common.ClientConfigFile);

                if (this.needRestart)
                {
                    if (this.isUpdating)
                    {
                        this.ShowMessage("Update Completed\n\nClick OK to restart the application", true);
                    }
                    else
                    {
                        this.ShowMessage("Download Completed\n\nClick OK to start the application", true);
                    }
                    Process.Start(Application.ExecutablePath);
                    Environment.Exit(0);

                    if (this.onUpdateCompleted != null)
                    {
                        this.onUpdateCompleted(this, null);
                    }
                    //Process.Start(this.GetType().Assembly.Location);
                    //Application.Exit();
                }
            }
        }

        /// <summary>
        /// Show error if not hidden
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="ex"></param>
        private void HandleException(string caption, Exception ex)
        {
            if (!this.isBackend)
            {
                if (this.isUpdating)
                {
                    MessageBox.Show(String.Format("{0}\n\n{1}", caption, ex.Message), "Auto Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(String.Format("{0}\n\n{1}", caption, ex.Message), "Auto Install", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
            }
        }

        /// <summary>
        /// Handle the different types of exception
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            if(ex is WebException)
            {
                this.HandleException("Network Connection Error", ex);
            }
            else if(ex is XmlException)
            {
                this.HandleException("Update Infomation Error", ex);
            }
            else if(ex is NotSupportedException)
            {
                this.HandleException("Update Configuration Error", ex);
            }
            else if(ex is ArgumentException)
            {
                this.HandleException("Download Update Files Error", ex);
            }
            else
            {
                this.HandleException("Failed to Update", ex);
            }
        }

        /// <summary>
        /// Show popup message
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            this.ShowMessage(message, false);
        }

        private void ShowMessage(string message, bool alwaysShow)
        {
            if (!this.isBackend || alwaysShow)
            {
                if (this.isUpdating)
                {
                    MessageBox.Show(message, "Auto Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(message, "Auto Install", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }//end of class
}
