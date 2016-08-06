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

namespace QLike.AutoUpdate
{
    public partial class CheckingForm : Form
    {
        private bool isUpdating = true;

        public CheckingForm(bool isUpdating)
        {
            this.isUpdating = isUpdating;
            InitializeComponent();
        }

        private void DownloadPrepare_Load(object sender, EventArgs e)
        {
            if (this.isUpdating)
            {
                this.Text = "Checking Updates...";
            }
            else
            {
                this.Text = "Preparing to Download...";
            }

            this.Activate();
            this.Focus(); 
        }
    }//end of class
}
