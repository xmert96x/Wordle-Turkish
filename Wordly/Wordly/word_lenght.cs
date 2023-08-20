using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Wordly
{

    public partial class word_length : Form
    {
        bool control= true;
        public word_length(int height, int width,int x,int y)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(x,y);

    

            comboBox1.Text = "Kelime Sayısını Seçmeyi Unutmayın";
            comboBox1.ForeColor = Color.Gray;


            this.Height = height;
            this.Width = width;
            string[] filenames = Directory.GetFiles(Wordly.Word.folderpath);
            List<int> letterCounts = new List<int>();
            foreach (string filename in filenames)
            {
                string tempfilename = Path.GetFileName(filename); // get the filename from the path
                int lettercount = 0;
                if (int.TryParse(tempfilename.Substring(0, 2), out lettercount))
                {letterCounts.Add(lettercount);
                }
                else if (int.TryParse(tempfilename.Substring(0, 1), out lettercount))
                {
                    letterCounts.Add(lettercount);
                }
                
              letterCounts.Sort(); 
            }
            foreach (int count in letterCounts)
            {
               comboBox1.Items.Add(count);
            }

            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            button1.Enabled = false;

        }


        private void word_length_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(control) { 
            DialogResult result = MessageBox.Show("Gerçekten kapatmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
              

                if (result == DialogResult.No)
            {
                e.Cancel = true;
                if (comboBox1.SelectedIndex == -1)
                {
                    errorProvider1.SetError(comboBox1, "Lütfen Harf Sayısını  Seçiniz");
                    comboBox1.Focus();
                    e.Cancel = true;

                }

                else
                {
                    button1.Focus();

                }

               
            }
                
            }





        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();

        }

            
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox1.ForeColor = Color.Black;
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                comboBox1.Text = "Kelime Sayısını Seçmeyi Unutmayın";
                comboBox1.ForeColor = Color.Gray;

             
            }




        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            
            PlayerManager.player.WordLength =Convert.ToInt16(comboBox1.GetItemText(comboBox1.SelectedItem));
            PlayerManager.SavePlayerData();
            messagebox box = new messagebox();

            control = !control;
            box.Show(); this.Close();

        }
    }
}


 