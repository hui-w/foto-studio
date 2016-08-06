namespace QLike.AutoUpdate
{
    partial class ProgressForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.lblDownloadPercentage = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelCurrentItem = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(14, 57);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(360, 13);
            this.progressBarTotal.Step = 1;
            this.progressBarTotal.TabIndex = 4;
            // 
            // lblDownloadPercentage
            // 
            this.lblDownloadPercentage.AutoSize = true;
            this.lblDownloadPercentage.Location = new System.Drawing.Point(11, 31);
            this.lblDownloadPercentage.Name = "lblDownloadPercentage";
            this.lblDownloadPercentage.Size = new System.Drawing.Size(113, 13);
            this.lblDownloadPercentage.TabIndex = 2;
            this.lblDownloadPercentage.Text = "Download Percentage";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(299, 95);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 25);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Cancel";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // labelCurrentItem
            // 
            this.labelCurrentItem.AutoSize = true;
            this.labelCurrentItem.Location = new System.Drawing.Point(11, 9);
            this.labelCurrentItem.Name = "labelCurrentItem";
            this.labelCurrentItem.Size = new System.Drawing.Size(64, 13);
            this.labelCurrentItem.TabIndex = 7;
            this.labelCurrentItem.Text = "Current Item";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 132);
            this.Controls.Add(this.labelCurrentItem);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.lblDownloadPercentage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Progress";
            this.Load += new System.EventHandler(this.DownloadProgress_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadProgress_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarTotal;
        private System.Windows.Forms.Label lblDownloadPercentage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelCurrentItem;
    }
}