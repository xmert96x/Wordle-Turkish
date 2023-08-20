using System.Drawing;
using System.Windows.Forms;
using System;

namespace Wordly
{
    public partial class looding_screen : Form
    {
        int i = 3;
        string text = "Yükleniyor";

        public looding_screen()
        {

         
            InitializeComponent();
        

            timer1.Enabled = true;
            this.TopMost = true;
            this.Text = "Yükleniyor";
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
            Console.WriteLine(text);
            label1.Text = text;
            for (int k = i; k > 0; k--)
            {
                label1.Text += ".";

            }

            i = (i == 0) ? 4 : i;
            i--;
        }

      
    }
}
