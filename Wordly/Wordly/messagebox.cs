using System.Drawing;
using System.Windows.Forms;
using System;
 namespace Wordly
{
    public partial class messagebox : Form
    {
        int i = 12;
        public messagebox()
        {
             InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.messagebox_FormClosing);
            this.TopMost = true;
            label1.Text = (i - 2).ToString() + " Saniye Bekleyiniz";
            this.Text = "Countdown";
            label1.Width = this.Width;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;
            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.FormBorderStyle = FormBorderStyle.None;
            label1.Anchor = AnchorStyles.None;
            label1.Location = new Point((this.ClientSize.Width - label1.Width) / 2, (this.ClientSize.Height - label1.Height) / 2);
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
       
            if (i > 3)
            {
                label1.Text = (i-3).ToString() + " Saniye Bekleyiniz";
            }
            else if (i>=0)
            {
                label1.Text = "Başlatılıyor";
                for (int k=1; k<=i; k++)
                { 
                    label1.Text += ".";
                }  
            }
            else this.Close();
            i--;
        }
        private void messagebox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i > 0) e.Cancel = true;
        }
    }
 }
 