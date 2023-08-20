using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wordly.Properties;

namespace Wordly
{
	public partial class Homepage : Form
    {
		int temph;
        public Homepage()
        { InitializeComponent();

            Word.CreateWordFiles();
            temph = this.Height;
            bool file = true;


            string filepath = Path.Combine(Application.StartupPath, "data.dat");

            if (!File.Exists(filepath))
            {

                PlayerManager.LoadPlayerData();
                file = false;
            }
            else
            {
                PlayerManager.LoadPlayerData();
                Console.WriteLine(PlayerManager.player.WordLength.ToString());

            }

            Console.WriteLine(PlayerManager.player.WordLength.ToString());
            if (PlayerManager.player.WordLength == 0 || file == true)
            {
                button2.Dispose();
                this.Height = button1.Top + button1.Height * 3;
            }





            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            label1.Location = new Point((this.ClientSize.Width - label1.Width) / 2, label1.Location.Y);
            button1.Location = new Point((this.ClientSize.Width - button1.Width) / 2, button1.Location.Y);
            button2.Location = new Point((this.ClientSize.Width - button2.Width) / 2, button2.Location.Y);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
 

        }
        private void button1_Click(object sender, EventArgs e)
		{
			word_length box = new word_length(temph, this.Width,this.Location.X, this.Location.Y);
            box.Show();
            this.Hide();
			
			 
		}
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
if (MessageBox.Show(  "Gerçekten Çıkmak istiyor musunuz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button2) == DialogResult.No) e.Cancel = true;

        }

   


    }
  	}
 