using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Resources;

namespace QLike.Foto.Launcher
{
    public partial class pnlApp : UserControl
    {
        public delegate void ClickHandler(object sender, string e);
        public event ClickHandler onClick;

        bool executable = true;
        private string appName;
        private string iconName;
        private string executablePath;

        public pnlApp(string appName, string iconName, string executablePath)
        {
            this.appName = appName;
            this.iconName = iconName;
            this.executablePath = executablePath;
            this.executable = !string.IsNullOrEmpty(executablePath);
            InitializeComponent();
        }

        private void pnlFotoGrid_Load(object sender, EventArgs e)
        {
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMain.BackColor = this.executable ? Color.LightGray : Color.LightBlue;
            this.pnlMain.Cursor = Cursors.Hand;
            
            this.lblAppName.Text = this.appName;

            //show logo
            Image img = Properties.Resources.ResourceManager.GetObject(this.iconName) as Image;
            if (img != null)
            {
                this.icon.Image = img;
            }

            //events
            foreach (Control c in this.pnlMain.Controls)
            {
                c.MouseUp += new MouseEventHandler(pnlMain_MouseUp);
                c.MouseEnter += new EventHandler(pnlMain_MouseEnter);
                c.MouseLeave += new EventHandler(pnlMain_MouseLeave);
            }
        }

        private void pnlMain_MouseEnter(object sender, EventArgs e)
        {
            this.pnlMain.BackColor = this.executable ? Color.LightSlateGray : Color.RosyBrown;
            this.lblAppName.ForeColor = Color.White;
        }

        private void pnlMain_MouseLeave(object sender, EventArgs e)
        {
            this.pnlMain.BackColor = this.executable ? Color.LightGray : Color.LightBlue;
            this.lblAppName.ForeColor = Color.Black;
        }

        private void pnlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.executablePath))
            {
                Process.Start(this.executablePath);
                Application.Exit();
            }
            else
            {
                if (this.onClick != null)
                {
                    this.onClick(this, string.Empty);
                }
            }
        }
    }//end of class
}
